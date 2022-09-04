$(document).ready(function () {
    loadFitnessCenters();


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
    $(document).on('click', '#fitness-centers-div th', function () {
        const th = $(this)[0];
        const table = th.closest('table');
        Array.from(table.querySelectorAll('tr:nth-child(n+2)'))
            .sort(comparer(Array.from(th.parentNode.children).indexOf(th), this.asc = !this.asc))
            .forEach(tr => table.appendChild(tr));
    });

    $(document).on('click', '.button-details', function () {
        sessionStorage.setItem("fc-id", $(this).attr('id'));
        window.location.href = 'FitnessCenterDetails.html';
    })


    $('#search-button').click(function () {
        var name = $('#search-name').val().toLocaleLowerCase();
        var address = $('#search-address').val().toLocaleLowerCase();
        
        var minYear = $('#search-min-year').val();
        if (minYear == "") { minYear = 1990;}
        var maxYear = $('#search-max-year').val();
        if (maxYear == "") { maxYear = 2022; }
        table = $('#fitness-centers-table tr:not(:first)').filter(function () {
            $(this).toggle(
                ($(this.children[0]).text().toLowerCase().indexOf(name) > -1) &&
                ($(this.children[1]).text().toLowerCase().indexOf(address) > -1) &&
                ($(this.children[2]).text().slice(-4) >= minYear) &&
                ($(this.children[2]).text().slice(-4) <= maxYear)
            )
        });
        
        

    });

   
});

function loadFitnessCenters() {
    $.ajax({
        url: '/api/FitnessCenters',
        method: 'GET',
        success: function (data) {
            var centers = data;
            if (centers.length == 0) {
                $('#fitness-centers-div').html("Trenutno nema fitnes centara");
            }
            else {
                let fitnesCentar = '<table id="fitness-centers-table" border="1px solid">';
                fitnesCentar += '<tr><th>Naziv</th><th>Adresa</th><th>Godina otvaranja</th><th></th></tr>';
                for (member in centers) {
              
                    d = new Date(centers[member].OpeningDate);
                    date = d.getDate() + "-" + (d.getMonth() + 1) + "-" + d.getFullYear();
                    fitnesCentar += '<tr><td>' + centers[member].Name + '</td><td>' + centers[member].Addresss.Street + '</td><td>' + date + '</td><td><button class="button-details" id="' + centers[member].Id+'">Detalji</button></td></tr>'
                }
                fitnesCentar += '</table>';
                $('#fitness-centers-div').html(fitnesCentar);
            }

        }
    });
}

