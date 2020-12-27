var AttendanceService = function () {
    var createAttendance = function (gigId, success, error) {
        $.ajax({
            url: "/api/Attendances",
            method: "POST",
            data: JSON.stringify({ gigId:gigId }),
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            },

            success: (success),
            error: (error)
        });
    };

    var deleteAttendance = function (gigId,success,error) {
        $.ajax({
            url: '/api/Attendances/Gigs/' + gigId,
            method: 'DELETE',
            success: (success),
            error: (error)
        });
    };

    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    };

}();


var FollowService = function () {
    var following = function (followeeId, success, error) {
        $.ajax
            ({
                url: "/api/Following",
                method: "POST",
                data: JSON.stringify({ FolloweeId: followeeId }),
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json'
                },

                success: (success),
                error: (error)
            });
    };

    var follow = function (followeeId, success, error) {
        $.ajax

            ({
                url: "/api/Following/" + followeeId,
                method: "DELETE",
                success: (success),
                error: (error)
            });
    };

    return {
        following: following,
        follow: follow
    };

}();



var GigsController = function (attendanceService) {
    var button;
    var init = function (container) {
        $(container).on("click", ".js_toggle_attendance", toggleAttendance);
    };
    var toggleAttendance = function (e) {
        button = $(e.target);
        var gigId = Number.parseInt(button.attr("data-gig-id"));
        if (button.hasClass("btn-light"))
            attendanceService.createAttendance(gigId, success, error);
        else
            attendanceService.deleteAttendance(gigId, success, error);
        
    };

    var success = function () {
        var text = (button.text() == 'Going') ? 'Going?' : 'Going';
        button.toggleClass('btn-light').toggleClass('btn-primary').text(text);

    };

    var error = function () {
        alert("Something Failed");
    };

    return {
        init: init
    }

}(AttendanceService);

var GigsDetailsController = function (followService)
{
    var button;

    var follow = function () {
        $(".js_toggle_following").click(toggleFollow);
    };

    var toggleFollow = function (e) {
        button = $(e.target);
        var followeeId = button.attr("data-artist-id");
        if (button.hasClass("btn-light"))
            followService.following(followeeId, success, error);
        else
            followService.follow(followeeId, success, error);
    };

var success = function () {
    var text = (button.text() == 'Following') ? 'Follow' : 'Following';
    button.toggleClass('btn-light').toggleClass('btn-primary').text(text);
};

var error = function () {
    alert("something failed")
};

return {
    follow: follow
}

}(FollowService);

