$(document).ready(function () {
    id = sessionStorage.getItem("fc-id");
    $.ajax({
        url: '/api/FitnessCenters/'+id,
        method: 'GET',
        success: function (data) {
            FillFCTable(data);
        }


    });
    $.ajax({
        url: '/api/GroupTrainings/' + id,
        method: 'GET',
        success: function (data) {
            FillGCTable(data);
        }


    });
    $.ajax({
        url: '/api/Comments/' + id,
        method: 'GET',
        success: function (data) {
            FillComments(data);
        }


    });

    


});

function FillComments(data) {

    if (data.length == 0) {
        var row = "<h1>NEMA KOMENTARA</h1>";
        $('#comments-details').append(row);
    }
    else {
        data.forEach(function (item) {
            var row = "<div class=\"comment\"><h3>" + item.Username + "</h3><br /><h4>Ocena: " + item.RatingGrade + "</h4><br /><p>" + item.Content + "</p></div>";
            $('#comments-details').append(row);
        });
    }

}


function FillGCTable(data) {
    var types = ["Yoga", "LesMillsTone", "BodyPump", "Cardio", "Other"];

    if (data.length == 0) {
        var row = "<tr><td colspan=\"5\">Trenutno nema treninga</td></tr>";
        $('#group-trainings-details-table').append(row);
    }
    else {
        data.forEach(function (item) {
            var pos = item.Visitors.length + "/" + item.Capacity;
            var row = "<tr><td>" + item.Id + "</td><td>" + types[item.Type] + "</td><td>" + item.Duration + " minuta</td><td>" + item.Appointment + "</td><td>" + pos + "</td></tr>";
            $('#group-trainings-details-table').append(row);
        });
    }

}

function FillFCTable(data) {
    $.ajax({
        url: '/api/User/' + data.OwnerId,
        method: 'GET',
        success: function (data) {
            
            $('#fcd-owner').html(data.Username);
        }


    });

    d = new Date( data.OpeningDate);
    date = d.getDate() + "-" + (d.getMonth() + 1) + "-" + d.getFullYear();
    $('#fcd-id').html(data.Id);
    $('#fcd-name').html(data.Name);
    $('#fcd-address').html(data.Addresss.Street); 
    $('#fcd-date').html(date);
    
    $('#fcd-fee-year').html(data.FeeYear);
    $('#fcd-fee-month').html(data.FeeMonth);
    $('#fcd-training').html(data.PriceTrainig);
    $('#fcd-training-group').html(data.PriceTrainingGroup);
    $('#fcd-training-private').html(data.PriceTrainingPrivate);

}


