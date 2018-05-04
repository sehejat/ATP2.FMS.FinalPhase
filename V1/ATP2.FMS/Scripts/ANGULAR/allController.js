﻿app.controller('CommentController', function ($scope,$http) {


    $scope.Message = "";
    $scope.Comments = [];
    $scope.PID = 0;

    $scope.Init = function(pid) {

        alert(pid);
        $scope.PID = pid;
        $scope.LoadComments();
    };
    $scope.LoadComments = function () {

        $http({
            method: "GET",
            url: "http://localhost:64944//api/Comments/" + $scope.PID
        }).then(function mySuccess(response) {
            alert("Success");
            console.log(response.data);
            $scope.Comments = response.data;
        }, function myError(response) {
            alert(response.statusText);
        });

    }

    $scope.NewComment = function() {

        var com = { UserId: 1, ProjectSectionId: $scope.PID, Commt: $scope.Message };

        $http.post("http://localhost:64944//api/Comments/", com).then(
            function (response) {
                alert('success');

                if (response.data)
                    $scope.Comments.push(response.data);

            }, function (response) {
                alert('error');
            });

    };
});