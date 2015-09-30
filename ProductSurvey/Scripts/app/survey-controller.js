angular.module('SurveyApp', [])
    .controller('SurveyCtrl', function ($scope, $http) {
        $scope.nextQuestion = function () {
            $scope.title = "loading question...";
            $scope.questionid = 0;
            $scope.surveyid = 1;
            $scope.options = [];
            $scope.isFront = true;

            $http.get("/api/survey")
                .success(function (data, status, headers, config) {
                    $scope.options = data.questionOptions;
                    $scope.title = data.questionText;
                    $scope.questionid = data.questionId;
                    $scope.surveyid = data.surveyId;
                    })
                .error(function (data, status, headers, config) {
                    $scope.title = "Oops... something went wrong";
                });
        };

        $scope.sendAnswer = function (option) {
            $http.post('/api/survey', { 'QuestionId': $scope.questionid, 'SelectedRate': option.optionRate, 'SurveyId': 1 })
                .success(function (data, status, headers, config) {
                    $scope.options = data.questionOptions;
                    $scope.title = data.questionText;
                    $scope.questionid = data.questionId;
                    $scope.surveyid = data.surveyId;
                    $scope.isFront = !$scope.isFront;
                }).error(function (data, status, headers, config) {
                    $scope.title = "Oops... something went wrong";
                });
        };
    });