angular.module('customerForm', ['ngFileUpload'])
    .controller('CustomerController', function ($scope, $http, Upload) {
        $scope.custModel = {};
        $scope.message = '';
        $scope.result = "color-default";
        $scope.isViewLoading = false;
        $scope.listCustomer = null;

        ////******=========Get All Customer=========******
        $scope.getAllCustomer = function () {
            $http.get('/api/customer')
                .then(function (response) {
                    var k = JSON.parse(JSON.stringify(response.data));
                    $scope.listCustomer = k;
                    console.log(response.data);
                }, function (error) {
                    $scope.message = 'Unexpected Error while loading data!!';
                    $scope.result = "color-red";
                    console.log($scope.message);
                });
        };

        $scope.getAllCustomer();


        //******=========Get Single Customer=========******
        $scope.getCustomer = function (custModel) {
            var url = '/api/customer/byid?id=' + custModel.id;
            $http.get(url)
                .then(function (response) {
                    $scope.custModel.Email = response.data.email
                    $scope.custModel.FirstName = response.data.firstName
                    $scope.custModel.LastName = response.data.lastName
                    $scope.custModel.Gender = response.data.gender
                    $scope.custModel.Phone = response.data.phone
                    $scope.custModel.DOB = response.data.dob
                    $scope.custModel.Password = "12345"
                    $scope.custModel.FileId = response.data.fileId
                    $scope.custModel.Id = response.data.id
                    var element = angular.element(document.querySelector('#dvProgress'));
                    $scope.Progress = 2;
                    element.html('');
                    element.html('<a href="/api/downloadfile?id=' + $scope.custModel.FileId + '">' + response.data.fileName + '</a>');
                }, function (error) {
                    $scope.message = 'Unexpected Error while loading data!!';
                    $scope.result = "color-red";
                    console.log($scope.message);
                });
        };

        //******=========Save Customer=========******

        $scope.saveCustomer = function () {
            if (!$scope.custModel.Id) { InsertCustomer(); }
            else { updateCustomer(); }
        };


        function InsertCustomer () {
            var model = {
                Email: $scope.custModel.Email,
                FirstName: $scope.custModel.FirstName,
                LastName: $scope.custModel.LastName,
                Gender: $scope.custModel.Gender,
                Phone: $scope.custModel.Phone.toString(),
                DOB: $scope.custModel.DOB,
                Password: $scope.custModel.Password,
                FileId: $scope.custModel?.FileId ?? 0,
                Fullname: "N/A",
                Filename: "N/A"
            };
            $http.post('/api/customer', model).then(function (response) {
                $scope.message = 'Record inserted successfully!';
                $scope.result = "color-green";
                $scope.custModel = {};
                $scope.isViewLoading = false;
                $scope.getAllCustomer();
                $scope.Progress = -1;
            }, function (error) {
                $scope.message = 'Unexpected Error while loading data!!';
                $scope.result = "color-red";
                console.log($scope.message);
            });
        };

        ////******=========Edit Customer=========******
        function updateCustomer () {
            var model = {
                Email: $scope.custModel.Email,
                FirstName: $scope.custModel.FirstName,
                LastName: $scope.custModel.LastName,
                Gender: $scope.custModel.Gender,
                Phone: $scope.custModel.Phone.toString(),
                DOB: $scope.custModel.DOB,
                Password: $scope.custModel.Password,
                FileId: $scope.custModel?.FileId ?? 0,
                Fullname: "N/A",
                Id: $scope.custModel.Id,
                Filename: "N/A"
            };

            $http.put('/api/customer', model).then(function (response) {
                $scope.message = 'Record updated successfully!';
                $scope.result = "color-green";
                $scope.custModel = {};
                $scope.isViewLoading = false;
                $scope.getAllCustomer();
                $scope.Progress = -1;

            }, function (error) {
                $scope.message = 'Unexpected Error while loading data!!';
                $scope.result = "color-red";
                console.log($scope.message);
            });
        };

        ////******=========Delete Customer=========******

        $scope.deleteCustomer = function (custModel) {
            var url = '/api/customer?id=' + custModel.id;
            var IsConf = confirm('You are about to delete ' + custModel.fullname + '. Are you sure?');
            if (IsConf) {
                $http.delete(url).then(function (response) {
                    $scope.message = $scope.custModel.FirstName + ' deleted from record!!';
                    $scope.result = "color-green";
                    $scope.custModel = {};
                    $scope.isViewLoading = false;
                    $scope.getAllCustomer();
                }, function (error) {
                    $scope.message = 'Unexpected Error while loading data!!';
                    $scope.result = "color-red";
                    console.log($scope.message);
                });
            }
        };

        $scope.UploadFiles = function (files) {
            $scope.SelectedFiles = files;
            if ($scope.SelectedFiles && $scope.SelectedFiles.length) {
                Upload.upload({
                    url: "/download/uploadFiles",
                    data: {
                        files: $scope.SelectedFiles
                    }
                }).then(function (response) {
                    $scope.custModel.FileId = response.data.pictureId;
                }, function (response) {
                    if (response.status > 0) {
                        var errorMsg = response.status + ': ' + response.data;
                        alert(errorMsg);
                    }
                }, function (evt) {
                    var element = angular.element(document.querySelector('#dvProgress'));
                    $scope.Progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                    element.html('<div style="width: ' + $scope.Progress + '%">' + $scope.Progress + '%</div>');
                });
            }

        }
    })
    .directive('onlyDigits', function () {
        return {
            require: 'ngModel',
            restrict: 'A',
            link: function (scope, element, attr, ctrl) {
                function inputValue(val) {
                    if (val) {
                        var digits = val.replace(/[^0-9.]/g, '');

                        if (digits.split('.').length > 2) {
                            digits = digits.substring(0, digits.length - 1);
                        }

                        if (digits !== val) {
                            ctrl.$setViewValue(digits);
                            ctrl.$render();
                        }
                        return parseFloat(digits);
                    }
                    return undefined;
                }
                ctrl.$parsers.push(inputValue);
            }
        };
    });