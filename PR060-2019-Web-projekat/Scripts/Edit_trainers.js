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
                "CenterIdWorking": $('#txt-center option:selected').val(),
                "BirthDate": $('#txt-date').val()
            };
            $.ajax({
                type: "POST",
                url: '/api/registration/Trainer',
                data: korisnik,
                success: function (data) {
                    alert("Uspesna registracija trenera");
                    window.location.href = 'Index.html';

                },
                error: function (data) {
                    alert(data.responseJSON['Message']);
                }


            });
        } else {
            alert("Lozinke se ne poklapaju");
        }
    });
    var user = {
        "UserName": sessionStorage.getItem("user_username")
    }
    $.ajax({
        type: "POST",
        url: '/api/User/GetTrainersForO',
        data: user,
        //dataType: "json",
        success: function (data) {
            FillTrainers(data);
        }

    });

    

    var user = {
        "UserName":sessionStorage.getItem("user_username")
    }
    $.ajax({
        type: "POST",
        url: '/api/FitnessCenters/fcForOwner',
        data: user,
        //dataType: "json",
        success: function (data) {
            FillCenters(data);
        }

    });


});

function FillTrainers(data) {
    if (data.length == 0) {
        var row = "<tr><td colspan=\"4\">Trenutno nema trenera</td></tr>";
        $('#trainer-table').append(row);
    } else {
        for (x of data) {
            var row = "<tr><td>" + x.Id + "</td><td>" + x.FirstName + " " + x.LastName + "</td><td>" + x.UserName + "</td>"
            if (x.Exist) {
                row += "<td><button class=\"ban-trainer\" id=\"" + x.Id + "\">Banuj</button></td>"
            } else {
                row += "<td>Banovan</td>"
            }
            row += "</tr>"
            $('#trainer-table').append(row);
        }
        $(document).on('click', '.ban-trainer', function () {
            
            id = {
                "Id": $(this).attr('id')
            }
            $.ajax({
                type: "POST",
                url: '/api/User/BanTrainer',
                data: id,
                success: function (data) {
                    alert("Uspesno banovan trener");
                    window.location.href = "Edit_trainers.html";
                }
            })


        })
    }
}

function FillCenters(data) {
    for (x of data) {
        var row = "<option value=\"" + x.Id + "\">" + x.Name + "</option>"
        $('#txt-center').append(row);
    }
}

