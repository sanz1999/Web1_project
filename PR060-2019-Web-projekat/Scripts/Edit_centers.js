$(document).ready(function () {
    $('#modify-center-div').toggle();
    $('#new-center-div').toggle();
    var user = {
        "UserName": sessionStorage.getItem("user_username")
    }
    $.ajax({
        type: "POST",
        url: '/api/FitnessCenters/FcForOwner',
        data: user,
        //dataType: "json",
        success: function (data) {
            FillCenters(data);
        }

    });
    $('#new-center-show').click(function () {
        $('#new-center-div').toggle();
    });
    $('#new-center-cancel').click(function () {
        $('#new-center-div').toggle();
    });
    $('#modify-center-cancel').click(function () {
        $('#modify-center-div').toggle();
    });
    $('#new-center-submit').click(function () {
        center = {
            "Name": $('#new-center-name').val(),
            "OpeningDate": $('#new-center-date').val(),
            "FeeMonth": $('#new-FeeMonth').val(),
            "FeeYear": $('#new-FeeYear').val(),
            "PriceTrainig": $('#new-PriceTrainig').val(),
            "PriceTrainingGroup": $('#new-PriceTrainingGroup').val(),
            "PriceTrainingPrivate": $('#new-PriceTrainingPrivate').val()
        }

        $.ajax({
            type: "POST",
            url: '/api/FitnessCenters/CreateCenter',
            data: center,
            //dataType: "json",
            success: function (data) {
                alert("Uspesno kreiran centar");
                window.location.href = 'Edit_centers.html';
            },
            error: function (data) {
                alert(data.responseJSON['Message']);
            }

        });
    });
    $('#modify-center-submit').click(function () {
        center = {
            "Id":sessionStorage.getItem("fc_id"),
            "Name":$('#modify-center-name').val(),
            "OpeningDate":$('#modify-center-date').val(),
            "FeeMonth":$('#modify-FeeMonth').val(),
            "FeeYear":$('#modify-FeeYear').val(),
            "PriceTrainig":$('#modify-PriceTrainig').val(),
            "PriceTrainingGroup":$('#modify-PriceTrainingGroup').val(),
            "PriceTrainingPrivate":$('#modify-PriceTrainingPrivate').val()
        }
        
        $.ajax({
            type: "POST",
            url: '/api/FitnessCenters/ModifyCenter',
            data: center,
            //dataType: "json",
            success: function (data) {
                alert("Uspesno izmenjen centar");
                window.location.href = 'Edit_centers.html';
            },
            error: function (data) {
                alert(data.responseJSON['Message']);
            }

        });
    });

    
});

function FillCenters(data) {
    
    for (x of data) {

        var row = "<tr><td>" + x.Id + "</td><td>" + x.Name + "</td><td>" + x.Addresss.Street+"</td><td>"+x.OpeningDate+"</td>";
        row += "<td> <button class=\"modify-center\" id=\"" + x.Id + "\">Izmeni</button><button class=\"delete-center\" id=\"" + x.Id +"\">Obrisi</button></td>";
        
        row += "</tr>";
        $('#centers-table').append(row);
    }
    $(document).on('click', '.delete-center', function () {

        fc = {
            "Id": $(this).attr('id')
        }
        $.ajax({
            url: '/api/FitnessCenters/DeleteCenter',
            type: 'DELETE',
            data: JSON.stringify(fc),
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                alert("Uspesno obrisan centar!");
                window.location.href = 'Edit_centers.html';

            },
            error: function (data) {
                alert(data.responseJSON['Message']);
            }
        })


    })
    $(document).on('click', '.modify-center', function () {
        fc = {
            "Id": $(this).attr('id')
        }
        $.ajax({
            type: "POST",
            url: '/api/FitnessCenters/GetSingle',
            data: fc,
            //dataType: "json",
            success: function (data) {
                DoModify(data);
            }

        });

    })

    
}
function DoModify(data) {
    $('#modify-center-div').toggle();
    sessionStorage.setItem("fc_id", data.Id);
    $('#modify-center-name').val(data.Name);
    $('#modify-center-date').val(data.OpeningDate);
    $('#modify-FeeMonth').val(data.FeeMonth);
    $('#modify-FeeYear').val(data.FeeYear);
    $('#modify-PriceTrainig').val(data.PriceTrainig);
    $('#modify-PriceTrainingGroup').val(data.PriceTrainingGroup);
    $('#modify-PriceTrainingPrivate').val(data.PriceTrainingPrivate);
}