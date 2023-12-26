$(document).ready(function () {
detailTest();

});
var detailTest = function (Id) {
    debugger;
    var Id = $("#hdnId").val();
    var model = { Id: Id };
    $.ajax({
        url: "/Reg/editTest",
        method: "post",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            var html = "";
            $("tblRegg tbody").empty();

            html += "<tr><td>" + response.model.Id +             
                "</td><td>" + response.model.Name +
                "</td><td><img src='../Content/Photo/" + response.model.Photo + "'style='max-width:100px;max-height:100px;'/>";
                "</td><td>" + response.model.Age +
                "</td><td>" + response.model.Address + "</td></tr>";


            $("#tblRegg tbody").append(html);
        }
    });
}