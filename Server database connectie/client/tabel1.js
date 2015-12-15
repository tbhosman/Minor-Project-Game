 $(document).ready(function() {
        var data = $.getJSON("http://drproject.twi.tudelft.nl:8086/overview", function(){
            var obj = JSON.parse(JSON.stringify(data));
            var table = '<table><thead><th>First Name</th><th>Last Name</th></thead><tbody>';
            $.each(obj, function() {
               table += '<tr><td>' + this['firstName'] + '</td><td>' + this['lastName'] + '</td></tr>';
            });
        table += '</tbody></table>';
        document.getElementById("datalist").innerHTML = table;
        //console.log(data);
        });
});