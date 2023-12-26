$(document).ready(function () {
    getTestList();
 /*   detailTest();*/

});
var SaveTest = function () {
    $formData = new FormData();
    var Image = document.getElementById('file-1');
    if (Image.files.length > 0) {
        for (var i = 0; i < Image.files.length; i++)
        {
            $formData.append('file-1' + i, Image.files[i]);
        }
    }
    var Id = $("#hdnId").val();
    var Name = $("#txtname").val();
    var Photo = $("#file-1").val();
    var Address = $("#txtadd").val();
    var Age = $("#txtage").val();

    $formData.append('Id', Id);
    $formData.append('Name', Name);
    $formData.append('Photo', Photo);
    $formData.append('Address', Address);
    $formData.append('Age', Age);


    $.ajax({
        url: "/Reg/SaveTest",
        method: "post",
        data: $formData,
        contentType:false,
        dataType: "json",
        processData: false,
        async: false,

        success: function (response) {
            location.reload();
            alert(response.message);
        },
    });
}
var getTestList = function ()
{
    debugger;
    var search = $("#txtsearch").val();
    var model = {
        Searchtxt: search
    }

    $.ajax({
        url: "/Reg/getTestList",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            var html = "";
            $("#tblReg tbody").empty();

            $.each(response.model, function (index, elementvalue) {
                html += "<tr><td>" + elementvalue.Id +
                    "</td><td>" + elementvalue.Name +
                    "</td><td><img src='../Content/Photo/" + elementvalue.Photo + "'style='max-width:100px;max-height:100px;'/>" +
                    "</td><td>" + elementvalue.Address +
                    "</td><td>" + elementvalue.Age +
                    "</td><td><input type='button' value='Delete' class='btn btn-danger' onclick='deleteTest(" + elementvalue.Id + ")'/><input type='submit' value='Edit' class='btn btn-success' onclick='editTest(" + elementvalue.Id + ")'/><input type='button' value='Detail' class='btn btn-success' onclick='detailTest(" + elementvalue.Id + ")'/></td></tr>";
            });
            $("#tblReg tbody").append(html);
            getTestList();
            
        }
    });
}

var deleteTest = function (Id)
{
    
    var model = { Id: Id };
    $.ajax({
        url: "/Reg/deleteTest",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            alert("Deleted");
            getTestList();
        },
        error: function (response) {
            alert(response.model);
        }
    });
}

var editTest = function (Id) {
    debugger;
    var model = { Id: Id };
    $.ajax({
        url: "/Reg/editTest",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            $("#hdnId").val(response.model.Id);
            $("#txtname").val(response.model.Name);  
            $("#file-1").val(response.model.Photo);
            $("#txtadd").val(response.model.Address);
            $("#txtage").val(response.model.Age);


        }
    });
}

var detailTest = function (Id)
{
    debugger;
    window.location.href = "/Reg/detailIndex?Id=" + Id;
}