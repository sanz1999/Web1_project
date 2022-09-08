$(document).ready(function () {
    owner = {
        "UserName": sessionStorage.getItem("user_username")
    };
    $.ajax({
        type: "POST",
        url: '/api/Comments/AllForOwner',
        data: owner,
        //dataType: "json",
        success: function (data) {
            FillComments(data);
        },
        error: function (data) {
            alert(data.responseJSON['Message']);
        }

    });




});

function FillComments(data) {
    if (data.length == 0) {
        var row = "<tr><td colspan=\"5\">Nema Komentara</td></tr>";
        $('#comments-table').append(row);
    } else {
        for (x of data) {
            var row = "<tr><td>" + x.Id + "</td><td>" + x.Username + "</td><td>" + x.RatingGrade + "</td><td>" + x.Content + "</td>";
            if (x.Approved) {
                row += "<td>Odobreno</td>"
            } else {
                row += "<td><button class=\"approve\" id=\""+x.Id+"\">Odobri</button></td>"
            }
            row += "</tr>";
            $('#comments-table').append(row);
        }
        $(document).on('click', '.approve', function () {
            id = $(this).attr('id')
            fc = {
                "Id": id
            }
            $.ajax({
                type: "POST",
                url: '/api/Comments/Approve',
                data: fc,
                //dataType: "json",
                success: function (data) {
                    alert("Uspesno odobren komentar");
                    window.location.href = 'Comments.html';
                }

            });
            

        })


    }
}