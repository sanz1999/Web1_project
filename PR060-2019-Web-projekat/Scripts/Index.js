$(document).ready(function () {
   var temp;

   $.ajax ({
    url: '/api/user',
        method: 'GET',
        success: function (data) {
            temp = data;
            sessionStorage.setItem("udar",data);
            $('#nav-sign_out').hide();
        }
   });


   
});

