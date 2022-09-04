$(document).ready(function () {
    var roles=["Visitor","Trainer","Owner","Unregistered"]
    var user = sessionStorage.getItem("user_username");
    var user_role = sessionStorage.getItem("user_type");
    if (user == null ) {
        //Sending request to determine which role is logged
        $.ajax({
            url: '/api/login',
            method: 'GET',
            success: function (data) {
                sessionStorage.setItem("user_username", data.Username);
                sessionStorage.setItem("user_type", roles[data.Role]);
                temp = data;
                if (temp.Role == 0) { Visitor(); }
                else if (temp.Role == 1) { Trainer(); }
                else if (temp.Role == 2) { Owner(); }
                else { Unregistered(); }
            }
        });
    } else {
       
        if (user_role == "Visitor") { Visitor(); }
        else if (user_role == "Trainer") { Trainer(); }
        else if (user_role == "Owner") { Owner(); }
        else { Unregistered(); }
    }





    //NAVIGATION
    $('#nav-index').click(function () {
        window.location.href = "Index.html";
    });
    $('#nav-history').click(function () {
        window.location.href = "History.html";
    });
    $('#nav-my-trainings').click(function () {
        window.location.href = "My_trainings.html";
    });
    $('#nav-edit-trainers').click(function () {
        window.location.href = "Edit_trainers.html";
    });
    $('#nav-edit-centers').click(function () {
        window.location.href = "Edit_centers.html";
    });
    $('#nav-registration').click(function(){
        window.location.href = "Registration.html";
    });

    $('#nav-login').click(function(){
        window.location.href = "Login.html";
    });

    $('#nav-profile').click(function () {
        window.location.href = "Profile.html";
    });
    $('#nav-sign_out').click(function () {
        $.ajax({
            type: "GET",
            url: '/api/login/logout',
            success: function (data) {
                alert("Uspesno odjavljeni");
                Unregistered();
                sessionStorage.setItem("user_username", "");
                sessionStorage.setItem("user_type", roles[3]);
                window.location.href = "Index.html";
            },
            error: function (data) {
                    alert("Neuspesno");
                } 
        });
        
    });

});

function Unregistered() {
    $("#nav-index").show();
    $("#nav-history").hide();
    $("#nav-my-trainings").hide();
    $("#nav-edit-trainers").hide();
    $("#nav-edit-centers").hide();
    $("#nav-registration").show();
    $("#nav-login").show();
    $("#nav-profile").hide();
    $("#nav-sign_out").hide();
     
}
function Visitor() {
    $("#nav-index").show();
    $("#nav-history").show();
    $("#nav-my-trainings").hide();
    $("#nav-edit-trainers").hide();
    $("#nav-edit-centers").hide();
    $("#nav-registration").hide();
    $("#nav-login").hide();
    $("#nav-profile").show();
    $("#nav-sign_out").show();

}
function Trainer() {
    $("#nav-index").show();
    $("#nav-history").show();
    $("#nav-my-trainings").show();
    $("#nav-edit-trainers").hide();
    $("#nav-edit-centers").hide();
    $("#nav-registration").hide();
    $("#nav-login").hide();
    $("#nav-profile").show();
    $("#nav-sign_out").show();

}
function Owner() {
    $("#nav-index").show();
    $("#nav-history").hide();
    $("#nav-my-trainings").hide();
    $("#nav-edit-trainers").show();
    $("#nav-edit-centers").show();
    $("#nav-registration").hide();
    $("#nav-login").hide();
    $("#nav-profile").show();
    $("#nav-sign_out").show();

}
