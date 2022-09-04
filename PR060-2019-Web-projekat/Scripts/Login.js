$(document).ready(function () {
    $('#loggin-button').click(function () {
        event.preventDefault();
        var roles = ["Visitor", "Trainer", "Owner", "Unregistered"]


        var korisnik = {
            "Username" : $('#txt-username').val(),
            "Password" : $('#txt-password').val()
        };

        $.ajax({
            type: "POST",
            url: '/api/login/loggin',
            data: korisnik,
            //dataType: "json",
            success: function (data) {
                if (data.Authenticated) {
                    sessionStorage.setItem("user_username", data.Username);
                    sessionStorage.setItem("user_type", roles[data.Role]);
                    window.location.href = 'Index.html';
                }
                else {
                    alert("Neispravno korisnicko ime ili lozinka");
                }
            },
        });
    });
    
});