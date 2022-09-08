$(document).ready(function () {
    $('#old-trainings').toggle();
    $('#new-trainings').toggle();
    $('#new-training-div').toggle();
    $('#modify-training-div').toggle();
    $('#visitors-div').toggle();
    var visible_old = false;
    var visible_modify = false;
    var visible_new_div = false;
    var visible_new = false;

    $('#new-training').click(function () {
        $('#new-training-div').toggle();
        if (visible_new_div == true) {
            visible_new_div = false;
           
        } else {
            visible_new_div = true;
            
        }
       
    });
    $('#show-new-trainigs').click(function () {
        $('#new-trainings').toggle();
        if (visible_new == true) {
            visible_new = false;
            $('#show-new-trainigs').html("Prikazi predstojece treninge")
        } else {
            visible_new = true;
            $('#show-new-trainigs').html("Sakri predstojece treninge")
        }
    });
    
    $('#show-old-trainigs').click(function () {
        $('#old-trainings').toggle();
        if (visible_old == true) {
            visible_old = false;
            $('#show-old-trainigs').html("Prikazi odradjene treninge")
        } else {
            visible_old = true;
            $('#show-old-trainigs').html("Sakri odradjene treninge")
        }

    });
    korisnik = {
        "Username": sessionStorage.getItem("user_username")
    }
    $.ajax({
        type: "POST",
        url: '/api/GroupTraining/GetPastForTrainer',
        data: korisnik,
        success: function (data) {
            FillOldGCTable(data);
        },
        error: function () {
            alert("Neuspesno ucitavanje treninga");
        }


    });
    $.ajax({
        type: "POST",
        url: '/api/GroupTraining/GetUpcomingForTrainer',
        data: korisnik,
        success: function (data) {
            FillNewGCTable(data);
        },
        error: function () {
            alert("Neuspesno ucitavanje treninga");
        }


    });
    $('#visitors-click').click(function () {
        $('#visitors-div').toggle();
    });
    $('#search-button').click(function () {
        var name = $('#search-name-training').val().toLocaleLowerCase();
        var type = $('#search-type-training').val().toLocaleLowerCase();
   
        var min_time = $('#search-min-time').val(); 
        if (min_time == "") {
            min_time = "00:00";
        }
        var max_time = $('#search-max-time').val();
        if (max_time == "") {
            max_time = "23:59";
        }

    


        table = $('#old-trainings-details-tablee tr:not(:first)').filter(function () {
            $(this).toggle(($(this.children[1]).text().toLowerCase().indexOf(name) > -1) &&
                ($(this.children[2]).text().toLowerCase().indexOf(type) > -1) &&
                ($(this.children[4]).text().slice(-5) >= min_time) &&
                ($(this.children[4]).text().slice(-5) <= max_time) 
                
                );

        })
    });
   

    //https://stackoverflow.com/questions/14267781/sorting-html-table-with-javascript taken from this site
    var getCellValue = (tr, idx) => tr.children[idx].innerText || tr.children[idx].textContent;
    var comparer = function (idx, asc) {
        return function (a, b) {
            return function (v1, v2) {
                return (v1 !== '' && v2 !== '' && !isNaN(v1) && !isNaN(v2))
                    ? v1 - v2
                    : v1.toString().localeCompare(v2);
            }(getCellValue(asc ? a : b, idx), getCellValue(asc ? b : a, idx));
        }
    };
    $(document).on('click', '#old-trainings-details-tablee th', function () {
        const th = $(this)[0];
        const table = th.closest('table');
        Array.from(table.querySelectorAll('tr:nth-child(n+2)'))
            .sort(comparer(Array.from(th.parentNode.children).indexOf(th), this.asc = !this.asc))
            .forEach(tr => table.appendChild(tr));
    });

    $('#new-training-submit').click(function () {
     
        gt = {
            "TrainingName": $('#new-training-name').val(),
            "Type": $('#new-training-type').val(),
            "Duration": $('#new-training-duration').val(),
            "Appointment": $('#new-training-datetime').val(),
            "Capacity": $('#new-training-capacity').val()
        }

        $.ajax({

            url: '/api/GroupTraining/CreateTraining',
            type: 'PUT',
            data: JSON.stringify(gt),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                alert("Uspesno kreiran trening");
                window.location.href = 'My_trainings.html';
            },
            error: function (data) {
                alert(data.responseJSON['Message']);
            }

        });
    });


    
    $('#new-training-cancel').click(function () {
        $('#new-training-div').toggle();
    });
    $('#modify-training-submit').click(function () {
       
        gt = {
            "Id": sessionStorage.getItem("gt_id"),
            "TrainingName": $('#modify-training-name').val(),
            "Type": $('#modify-training-type').val(),
            "CenterId": sessionStorage.getItem("fc_id"),
            "Duration": $('#modify-training-duration').val(),
            "Appointment": $('#modify-training-datetime').val(),
            "Capacity": $('#modify-training-capacity').val()
        }
        $.ajax({

            url: '/api/GroupTraining/ModifyTraining',
            type: 'PUT',
            data: JSON.stringify(gt),
            contentType: 'application/json; charset=utf-8',
            success: function () {
                alert("Uspesno izmenjen trening");
                window.location.href = 'My_trainings.html';
            },
            error: function (data) {
                alert(data.responseJSON['Message']);
            }

        });
    });
    

});

function FillOldGCTable(data) {
    var types = ["Yoga", "LesMillsTone", "BodyPump", "Cardio", "Other"];

    if (data.length == 0) {
        var row = "<tr><td colspan=\"5\">Trenutno odrzanih nema treninga</td></tr>";
        $('#old-trainings-details-tablee').append(row);
    }
    else {
        for (name in data) {
            fcname = name.replace('(', ' ');
            fcname = fcname.replace(')', ' ');
            arrayy = fcname.split(",");
            d = new Date(data[name].Appointment);
            date = d.getDate() + "-" + (d.getMonth() + 1) + "-" + d.getFullYear() + " / " + data[name].Appointment.slice(-8).substring(0, 5);
            var pos = data[name].Visitors.length + "/" + data[name].Capacity;
            var row = "<tr><td>" + arrayy[1].trim() + "</td><td>" + data[name].TrainingName + "</td><td>" + types[data[name].Type] + "</td><td>" + data[name].Duration + " minuta</td><td>" + date + "</td><td>" + pos + "</td>";
            row += "<td><button class=\"show-visitors\" id=\"" + data[name].Id + "\">Posetioci</button></td>";
            row += "</tr > "
            $('#old-trainings-details-tablee').append(row);
        }
        
    }
}
function FillNewGCTable(data) {
    var types = ["Yoga", "LesMillsTone", "BodyPump", "Cardio", "Other"];

    if (data.length == 0) {
        var row = "<tr><td colspan=\"5\">Trenutno odrzanih nema treninga</td></tr>";
        $('#new-trainings-details-tablee').append(row);
    }
    else {
        for (name in data) {
            fcname = name.replace('(', ' ');
            fcname = fcname.replace(')', ' ');
            arrayy = fcname.split(",");
            d = new Date(data[name].Appointment);
            date = d.getDate() + "-" + (d.getMonth() + 1) + "-" + d.getFullYear() + " / " + data[name].Appointment.slice(-8).substring(0, 5);
            var pos = data[name].Visitors.length + "/" + data[name].Capacity;
            var row = "<tr><td>" + arrayy[1].trim() + "</td><td>" + data[name].TrainingName + "</td><td>" + types[data[name].Type] + "</td><td>" + data[name].Duration + " minuta</td><td>" + date + "</td><td>" + pos + "</td>";
            row += "<td> <button class=\"delete-training\" id=\"" + data[name].Id + "\">Obrisi</button>";
            row += " <button class=\"modify-training\" id=\"" + data[name].Id + "\">Izmeni</button></td>";
            row += "<td><button class=\"show-visitors\" id=\"" + data[name].Id + "\">Posetioci</button></td>";
            row += "</tr > "
            $('#new-trainings-details-tablee').append(row);
        }
        $(document).on('click', '.delete-training', function () {
            
            id = {
                "Id": $(this).attr('id')
            }
            $.ajax({
                url: '/api/GroupTraining/DeleteTraining',
                type: 'DELETE',
                data: JSON.stringify(id),
                contentType: 'application/json; charset=utf-8',
                success: function () {
                    alert("Uspesno obrisan trening!");   
                },
                error: function (data) {
                    alert(data.responseJSON['Message']);
                }
            })

        })
        $(document).on('click', '.modify-training', function () {
            sessionStorage.setItem("gt_id", $(this).attr('id'));
            id = {
                "Id": $(this).attr('id')
            }
            $.ajax({
                type: "POST",
                url: '/api/GroupTraining/SingleTraining',
                data: id,
                success: function (data) {
                    doModify(data);
                }
            })
            

        })
        $(document).on('click', '.show-visitors', function () {
            sessionStorage.setItem("gt_id", $(this).attr('id'));
            id = {
                "Id": $(this).attr('id')
            }
            $.ajax({
                type: "POST",
                url: '/api/User/VisitorsForTraining',
                data: id,
                success: function (data) {
                    FillVisitors(data);
                }
            })


        })
    }
}

function FillVisitors(data) {
    $('#visitors-div').toggle();
    if (data.length == 0) {
        var row = "<tr><td><button id=\"close-visitors\">Zatvori</button></td></tr><tr><th>Posetioci</th></tr>";
        row += "<tr><td colspan=\"1\">Trenutno nema posetioca</td></tr>";
        $('#visitors-table').html(row);
    } else {
        var row = "<tr><th>Posetioci</th></tr>";
        for (x of data) {
             row += "<tr><td>" + x.FirstName + " " + x.LastName + "</td></tr>";
            
        }
        $('#visitors-table').html(row);
    }
    

}

function doModify(data) {
    $('#modify-training-name').val(data.TrainingName);
    $('#modify-training-type').val(data.Type);
    sessionStorage.setItem("fc_id", data.CenterId);
    $('#modify-training-duration').val(data.Duration);
    $('#modify-training-datetime').val(data.Appointment);
    $('#modify-training-capacity').val(data.Capacity);  
    $('#modify-training-div').toggle();
    
}