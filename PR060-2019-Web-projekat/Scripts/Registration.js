$(document).ready(function () {
    $('#registration-button').click(function () {
        event.preventDefault();
        //alert("click");
        
        if ($('#txt-password').val() === $('#txt-confirm-password').val()) {

            var korisnik = {
                "Username": $('#txt-username').val(),
                "Password": $('#txt-password').val(),
                "FirstName": $('#txt-fname').val(),
                "LastName": $('#txt-lname').val(),
                "Gender": $('#gender').val(),
                "EMail": $('#txt-email').val(),
                "BirthDate": $('#txt-date').val()
            };
            $.ajax({
                type: "POST",
                url: '/api/registration',
                data: korisnik,
                //dataType: "json",
                success: function () {
                    alert("Uspesna registracija");
                    window.location.href = 'Index.html';
                },
                error: function (request) {
                    if (request.status == 400) {
                        alert("Greska");
                        }
                    else {
                        alert("Greska  2");
                    }
                }
                
            });
        } else {
            alert("Lozinke se ne poklapaju");
        }
    });

    
        
    
    
    
});

