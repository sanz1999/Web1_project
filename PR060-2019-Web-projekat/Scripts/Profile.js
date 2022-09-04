$(document).ready(function () {
    user_username = sessionStorage.getItem("user_username");
    korisnik = {
        "Username":user_username
    };
    var temp;
    $.ajax({
        type: "POST",
        url: '/api/User/CompleteUser',
        data: korisnik,
        //dataType: "json",
        success: function (data) {
            sessionStorage.setItem("user_id", data.Id);
            FillData(data);
          
        }

    });
    $('#profile-save-button').click(function () {
        event.preventDefault();
        var korisnik = {
            "Username": $('#txt-username').val(),
            "Password": $('#txt-password').val(),
            "FirstName": $('#txt-fname').val(),
            "LastName": $('#txt-lname').val(),
            "Gender": $('#gender').val(),
            "EMail": $('#txt-email').val(),
            "BirthDate": $('#txt-date').val(),
            "Id":sessionStorage.getItem("user_id")
        };

         $.ajax({
            
             url: '/api/registration/update',
             type: 'PUT',
             data: JSON.stringify(korisnik),
             contentType: 'application/json; charset=utf-8',
             dataType: 'json',
            success: function () {
                alert("Uspesno sacuvano");
                window.location.href = 'Index.html';
            },
            error: function (data) {
                alert(data.responseJSON['Message']);
            }

         });
    });

});

function FillData(data) {
    $('#txt-username').val(data.UserName);
    $('#txt-fname').val(data.FirstName);
    $('#txt-lname').val(data.LastName);
    $('#txt-email').val(data.EMail);
}

