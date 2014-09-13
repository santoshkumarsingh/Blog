function AppViewModel() {
    var self = this;
    //Since i hide and show the form to prevent turnaround from the server I created an observable property which is bound to the visibility of the form element.
    self.formVisible = ko.observable(false);

    //Here I instantiated all the properties bound to the input elements.
    self.firstName = ko.observable('');
    self.lastName = ko.observable('');
    self.address = ko.observable('');
    self.phone = ko.observable('');
    self.creditLimit = ko.observable('');
    self.customerSince = ko.observable('');
    self.customerId = ko.observable('');

    //Another property to toggle between add new and edit functionality.
    self.isEdit = ko.observable(false);

    //A function for showing the form which is called from the UI.
    self.showForm = function () {
        self.formVisible(true);
        self.resetForm();
        self.isEdit(false);
    };

    //A function for hiding the form which is called from the UI.
    self.hideForm = function () {
        self.formVisible(false);
    };

    //A function for resetting the values of the form.
    self.resetForm = function () {
        self.firstName('');
        self.lastName('');
        self.address('');
        self.phone('');
        self.creditLimit('');
        self.customerSince('');
    };

    //A function for getting the correct date.
    self.getCorrectDate = function (rawDate) {
        var dateStringValue = rawDate;
        var value = new Date (parseInt(dateStringValue.replace(/(^.*\()|([+-].*$)/g, '')));
        var correctDate = value.getFullYear() + '-' + value.getMonth() + 1 + "-" + value.getDate();
        return correctDate;
    }

    //A function which updates a customer
    self.editCustomer = function (id) {
        $.ajax({
            url: '/Home/GetCustomer',
            data: {
                customerId: id
            },
            success: function (data) {
                self.formVisible(true);
                self.isEdit(true);
                self.firstName(data.customer.FirstName);
                self.lastName(data.customer.LastName);
                self.address(data.customer.Address);
                self.phone(data.customer.Phone);
                self.creditLimit(data.customer.CreditLimit);
                self.customerSince(self.getCorrectDate(data.customer.CustomerSince));
                self.customerId(data.customer.Id);
            }
        });
    };

    //A function for deleting a customer
    self.deleteCustomer = function (id) {
        $.ajax({
            url: '/Home/DeleteCustomer',
            data: {
                customerId : id
            },
            success: function (data) {
                $("#customerlist").html(data);
            }
        });
    }

    //A function for loading the customer view via ajax.
    self.loadCustomers = function () {
        $.ajax({
            url: '/Home/CustomerList',
            success: function (data) {
                $("#customerlist").html(data);
            }
        });
    };

    //The function for updating and adding a customer.
    self.submitForm = function () {
        var url = '/Home/SaveCustomer';
        if (self.isEdit()) {
            url = '/Home/UpdateCustomer';
        } 
        $.ajax({
            url: url,
            data: {
                firstName: self.firstName(),
                lastName: self.lastName(),
                address: self.address(),
                phone: self.phone(),
                creditLimit: self.creditLimit(),
                customerSince: self.customerSince(),
                id: self.customerId()
            },
            success: function (data) {
                if (String(data.saved) == 'true') {
                    self.formVisible(false);
                    self.loadCustomers();
                }
            }
        });
    };
}

//Now here is the important part. Tell knockout to apply the necessary bindings between our dom elements and the above model.
ko.applyBindings(new AppViewModel());