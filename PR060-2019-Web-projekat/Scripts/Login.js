$(document).ready(function () {
    $('#loggin-button').click(function () {
        event.preventDefault();
        //alert("click");

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
                    window.location.href = 'Index.html';
                }
                else {
                    alert("Neispravno korisnicko ime ili lozinka");
                }
            },
        });
    });
    
});