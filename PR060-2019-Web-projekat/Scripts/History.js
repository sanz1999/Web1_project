$(document).ready(function () {
    korisnik = {
        "Username": sessionStorage.getItem("user_username")
    }
    $.ajax({
        type: "POST",
        url: '/api/GroupTraining/GetForVisitor',
        data: korisnik,
        success: function (data) {
            FillGCTable(data);
        },
        error: function () {
            alert("Neuspesno ucitavanje treninga");
        }


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
    $(document).on('click', '#group-trainings-details-tablee th', function () {
        const th = $(this)[0];
        const table = th.closest('table');
        Array.from(table.querySelectorAll('tr:nth-child(n+2)'))
            .sort(comparer(Array.from(th.parentNode.children).indexOf(th), this.asc = !this.asc))
            .forEach(tr => table.appendChild(tr));
    });

    $('#search-button').click(function () {
        var name = $('#search-name-training').val().toLocaleLowerCase();
        var type = $('#search-type-training').val().toLocaleLowerCase();
        var center = $('#search-name-center').val().toLocaleLowerCase();
        
        table = $('#group-trainings-details-tablee tr:not(:first)').filter(function () {
            $(this).toggle(
                ($(this.children[0]).text().toLowerCase().indexOf(center) > -1) &&
                ($(this.children[1]).text().toLowerCase().indexOf(name) > -1) &&
                ($(this.children[2]).text().toLowerCase().indexOf(type) > -1)
            )
        });

        

    });
    $('#make-comment').click(function () {
        event.preventDefault();
        if ($('#comment-content').val() != null && $('#comment-content').val() != "") {
            var comment = {
                "CenterId": sessionStorage.getItem("fc_id"),
                "Username": sessionStorage.getItem("user_username"),
                "Content": $('#comment-content').val(),
                "RatingGrade": $('#comment-grade').val(),
            }
            $.ajax({
                url: '/api/Comments/NewComment',
                type: 'PUT',
                data: JSON.stringify(comment),
                contentType: 'application/json; charset=utf-8',
                success: function () {
                    alert("Uspesno poslat komentar, bice vidljiv nakon sto vlasnik odobri");
                    $('#new-comment').removeClass("active");
                    $('#new-comment').addClass("hidden");
                },
                error: function (data) {
                    alert(data.responseJSON['Message']);
                }
            });
        } else {
            alert("Popuni polje komentar:");
        }


    });
    $('#cancel-comment').click(function () {
        $('#new-comment').removeClass("active");
        $('#new-comment').addClass("hidden");
    });


    


});

function FillGCTable(data) {
    var types = ["Yoga", "LesMillsTone", "BodyPump", "Cardio", "Other"];

    if (data.length == 0) {
        var row = "<tr><td colspan=\"5\">Trenutno posecenih nema treninga</td></tr>";
        $('#group-trainings-details-table').append(row);
    }
    else {
        for (name in data) {
            fcname = name.replace('(', ' ');
            fcname = fcname.replace(')', ' ');
            arrayy = fcname.split(",");
            d = new Date(data[name].Appointment);
            date = d.getDate() + "-" + (d.getMonth() + 1) + "-" + d.getFullYear() + " / " + data[name].Appointment.slice(-8).substring(0,5);
            var pos = data[name].Visitors.length + "/" + data[name].Capacity;
            var row = "<tr><td>" + arrayy[1].trim() + "</td><td>" + data[name].TrainingName + "</td><td>" + types[data[name].Type] + "</td><td>" + data[name].Duration + " minuta</td><td>" + date + "</td><td>" + pos + "</td>";
            row += "<td> <button class=\"comment-button\" name=\"" + arrayy[1].trim()+"\" id=\""+data[name].CenterId+"\">Novi Komentar</button></td>"
            row += "</tr > "
            $('#group-trainings-details-tablee').append(row);           
        }
        $(document).on('click', '.comment-button', function () {
            sessionStorage.setItem("fc_id", $(this).attr('id'));
            $('#comment-username').html(sessionStorage.getItem("user_username"));
            $('#comment-fc-name').html($(this).attr("name"));
            $('#comment-grade').val(5);
            $('#comment-content').val("");
            $('#new-comment').removeClass("hidden");
            $('#new-comment').addClass("active");


        })
        
    }
}

