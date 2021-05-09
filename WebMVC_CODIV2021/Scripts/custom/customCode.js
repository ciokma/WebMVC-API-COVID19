
//Here is all the code related with the view GetCovidCases
$("#btnReport").click(searchReport);
$("#btnXML").click(exportXML);
$("#btnJSON").click(exportJSON);
$("#btnCSV").click(exportCSV);
function validateComboSelected() {
    var region = $('#ddlRegions option:selected').text();

    if (region === "REGION") {
        alert("Please select a valid region to continue!");
        return false;
    }
    else
        return true;

}
function exportXML() {
    if (validateComboSelected()) {
        var region = $('#ddlRegions option:selected').text();
        window.location.href = "/CovidInfo/ExportXML?filter=" + region;
    }
    
}
function exportJSON() {
    if (validateComboSelected()) {
        var region = $('#ddlRegions option:selected').text();
        window.location.href = "/CovidInfo/ExportJSON?filter=" + region;
    }
    
}
function exportCSV() {
    if (validateComboSelected()) {
        var region = $('#ddlRegions option:selected').text();
        window.location.href = "/CovidInfo/ExportCSV?filter=" + region;
    }
   
}
function loadDefaultData() {
    var method = "POST";

    $.ajax({
        url: '/CovidInfo/GetAllCovidCases',
        type: method
    }).done(function (data) {
        alertify.success("The information has been received!");
        generateTable(data);


    }).fail(function (jqXHR, textStatus, errorThrown) {
        alertify.error(jqXHR.responseText || textStatus);
    });
}
function searchReport() {
    var region = $('#ddlRegions option:selected').text();
    var method = "POST";
    if (validateComboSelected()) {
        $("#loader").show();
        $("#btnReport").attr("disabled", "disabled");

        $.ajax({
            url: '/CovidInfo/GetCovidCasesByFilter',
            type: method,
            data: { filter: region }
        }).done(function (data) {
            alertify.success("The information has been received!");
            generateTableIso(data);
            $("#loader").hide();


        }).fail(function (jqXHR, textStatus, errorThrown) {
            alertify.error(jqXHR.responseText || textStatus);
        });
        $("#btnReport").removeAttr("disabled");

    }
}
function loadComboRegions() {
    var method = "POST";
    $.ajax({
        url: '/CovidInfo/GetRegions',
        type: method
    }).done(function (data) {
        var $dropdown = $("#ddlRegions");

        $.each(data, function () {
            $dropdown.append($("<option />").val(this.Name).text(this.iso));
        });

    }).fail(function (jqXHR, textStatus, errorThrown) {
        alertify.error(jqXHR.responseText || textStatus);
    });
}

$(document).ready(function () {
    loadDefaultData();
    loadComboRegions();
    alertify.success("Please wait, We're loading data...");


});


function generateTable(data) {

    $("#gridTable tbody").empty();
    var tableelement = $("#gridTable tbody");

    for (var i = 0; i < data.length; i++) {
        tableelement.append(createRow(data[i]));
    }

}
function createRow(rowObject) {
    var trElement = "<tr id='" + rowObject.iso + "'>";
    trElement += "<td>" + rowObject.iso + "</td>";
    trElement += "<td>" + numberWithCommas(rowObject.confirmed) + "</td>";
    trElement += "<td>" + numberWithCommas(rowObject.deaths) + "</td>";
    trElement += " </tr>";
    return trElement;
}
function generateTableIso(data) {

    $("#gridTable tbody").empty();
    var tableelement = $("#gridTable tbody");

    for (var i = 0; i < data.length; i++) {
        tableelement.append(createRowIso(data[i]));
    }

}
function createRowIso(rowObject) {
    if (rowObject.province === "" || rowObject.province === null) {
        rowObject.province = "N/A";
    }
    
    $("#regionHeader").html("<b>PROVINCE</b>");
    var trElement = "<tr id='" + rowObject.province + "'>";
    trElement += "<td>" + rowObject.province + "</td>";
    trElement += "<td>" + numberWithCommas(rowObject.confirmed) + "</td>";
    trElement += "<td>" + numberWithCommas(rowObject.deaths) + "</td>";
    trElement += " </tr>";
    return trElement;
}
function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}