$(document).ready(function () {
    document.getElementById("subCategory").disabled = true;
    document.getElementById("Size").disabled = true;

});

var Customers = []
//fetch categories from database
function LoadCustomerList(element) {
    if (Customers.length == 0) {
        //ajax function for fetch data
        $.ajax({
            type: "GET",
            url: '/SalesOrder/getCustomerList',
            success: function (data) {
                Customers = data;

                //render catagory
                renderCustomers(element);

            }
        })
    }
    else {
        //render catagory to the element
        renderCustomers(element);
    }
}

function renderCustomers(element) {
    var $ele = $(element);
    $ele.empty();

    $ele.append($('<option/>').val('0').text('Select'));
    $.each(Customers, function (i, val) {

        $($ele).append($('<option/>', {
            value: val.Id,
            text: val.Name
        }));


        //$ele.append($('<option/>').val(val.Id).text(val.Name));

    });

}
function LoadCustomerName() {
    if ($('#CustomerList').val() == 0) {
        document.getElementById('customername').value = "";
    }
    else {
        var customername = document.getElementById('CustomerList');
        var CustomerName = customername.options[customername.selectedIndex].text;

        $('#customername').text(CustomerName);


    }


}

var Categories = []
//fetch categories from database
function LoadproductCategory(category) {
    if (Categories.length == 0) {
        //ajax function for fetch data
        $.ajax({
            type: "GET",
            url: "/SalesOrder/getCategoryList",
            success: function (data) {
                Categories = data;
                //render catagory
                renderCategory(category);
            }
        })
    }
    else {
        //render catagory to the element
        renderCategory(category);
    }
}

function renderCategory(category) {
    var $ele = $(category);
    $ele.empty();
    $ele.append($('<option>').val('0').text('Select'));
    $.each(Categories, function (i, val) {

        $($ele).append($('<option>', {
            value: val.Id,
            text: val.Name
        }));

    })
}
//fetch products
function LoadSubCategory(categoryDD) {



    $.ajax({
        type: "GET",
        url: "/SalesOrder/getSubCategoryList",
        data: { 'CategoryID': $(categoryDD).val() },
        success: function (data) {
            //render products to appropriate dropdown
            renderSubCategory($(categoryDD).parents('.table').find('select.sb'), data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}

function renderSubCategory(element, data) {
    document.getElementById("subCategory").disabled = false;


    //render product
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));

    $.each(data, function (i, val) {

        $($ele).append($('<option>', {
            value: val.Id,
            text: val.Name
        }));
    })

}

function LoadItem(item) {

    $.ajax({
        type: "GET",
        url: "/SalesOrder/getItemList",
        data: { 'SubCategoryID': $(item).val() },
        success: function (data) {
            //render products to appropriate dropdown
            renderItem($(item).parents('.table').find('select.item'), data);
        },
        error: function (error) {

            console.log(error);
        }
    })
}


function renderItem(element, data) {

    //render product
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));

    $.each(data, function (i, val) {

        $($ele).append($('<option>', {
            value: val.Id,
            text: val.Name
        }));
    })

}


function LoadSize(item) {
    document.getElementById("Size").disabled = false;
    $.ajax({
        type: "GET",
        url: "/SalesOrder/getSizeList",
        data: { 'ItemId': $(item).val() },
        success: function (data) {
            //render products to appropriate dropdown
            renderSize($(item).parents('.table').find('select.size'), data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}
function renderSize(element, data) {
    //render product
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select'));
    $.each(data, function (i, val) {
        $($ele).append($('<option>', {
            value: val.Id,
            text: val.Size
        }));
    })
}





$(document).ready(function () {
    var isAllValid = true;
    //Add button click event
    $('#add').click(function () {
        $('#orderItemSuccess').text('');
        //validation and add order items
        if ($('#productCategory').val() == "0") {
            isAllValid = false;

            $('#productCategory').siblings('span.error').css('visibility', 'visible');
        }
        else {
            isAllValid = true;
            $('#productCategory').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#CustomerList').val() == "0") {
            isAllValid = false;
            $('#CustomerList').siblings('span.error').css('visibility', 'visible');
        }
        else {
            isAllValid = true;
            $('#CustomerList').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#subCategory').val() == "0") {
            isAllValid = false;
            $('#subCategory').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#subCategory').siblings('span.error').css('visibility', 'hidden');
        }
        if ($('#Item').val() == "0") {
            isAllValid = false;
            $('#Item').siblings('span.error').css('visibility', 'visible');
        }
        else {
            isAllValid = true;
            $('#Item').siblings('span.error').css('visibility', 'hidden');
        }
        if ($('#Size').val() == "0") {
            isAllValid = false;
            $('#Size').siblings('span.error').css('visibility', 'visible');
        }
        else {
            isAllValid = true;
            $('#Size').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#quantity').val().trim() != '' && (parseInt($('#quantity').val()) || 0))) {
            isAllValid = false;
            $('#quantity').siblings('span.error').css('visibility', 'visible');
        }
        else {
            isAllValid = true;
            $('#quantity').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#quantity').val().trim() != '' && (parseInt($('#quantity').val()) || 0))) {
            isAllValid = false;
            $('#quantity').siblings('span.error').css('visibility', 'visible');
        }
        else {
            isAllValid = true;
            $('#quantity').siblings('span.error').css('visibility', 'hidden');
        }
        if (!($('#OrderValue').val().trim() != '' && (parseInt($('#OrderValue').val()) || 0))) {
            isAllValid = false;
            $('#OrderValue').siblings('span.error').css('visibility', 'visible');
        }
        else {
            isAllValid = true;
            $('#OrderValue').siblings('span.error').css('visibility', 'hidden');
        }


        if (isAllValid == true) {
            $('#submit').attr('disabled', false);
            var $newRow = $('#mainrow').clone().removeAttr('id');
            $('.pc', $newRow).val($('#productCategory').val());
            $('.sb', $newRow).val($('#subCategory').val());
            $('.size', $newRow).val($('#Size').val());
            $('.item', $newRow).val($('#Item').val());
            $('.quantity', $newRow).val($('#quantity').val());

            //Replace add button with remove button
            $('#add', $newRow).addClass('remove').val('Remove').removeClass('btn-success').addClass('btn-danger');

            //remove id attribute from new clone row
            $('#Item,#Size,#productCategory,#subCategory,#add', $newRow).removeAttr('id');
            $('span.error', $newRow).remove();
            //append clone row
            $('#orderdetailsItems').append($newRow);
            var orderVal = Number($('#OrderValue').val());
            var totalOrderVal = Number($('#totalOrderValue').val());
            totalOrderVal = totalOrderVal + orderVal;
            document.getElementById('totalOrderValue').value = totalOrderVal;
            ////clear select data
            //$('#Item,#Size,#productCategory,#subCategory,#unitOFMeasure').val('0');
            //$('#quantity').val('');
            //$('#orderItemError').empty();
        }
        else{
            $('#submit').attr('disabled', true);
        }

    })

    //remove button click event
    $('#orderdetailsItems').on('click', '.remove', function () {
        var orderVal = Number($(this).parents('tr').find('#OrderValue').val());
        var totOrderVal = Number(document.getElementById('totalOrderValue').value);
        var tot = totOrderVal - orderVal;
        document.getElementById('totalOrderValue').value = tot;
        $(this).parents('tr').remove();

    });

    $('#submit').click(function () {

        $(this).attr('disabled', true);
        var isAllValid = true;


        //validate order items
        $('#orderItemError').text('');
        var list = [];
        var errorItemCount = 0;
        $('#orderdetailsItems tr').each(function (index, ele) {
            if (
                $('select.item', this).val() == "0" || $('select.pc', this).val() == "0" || $('#CustomerList').val() == "0" ||
                $('select.sb', this).val() == "0" ||
                $('select.size', this).val() == "0" ||
                (parseInt($('.quantity', this).text))) {
                errorItemCount++;
                $(this).addClass('error');
                if (('.quantity', this).val == "0") {
                    $('#orderItemError').text("Quantity can't be 0");
                    $('#orderDate').siblings('span.error').css('visibility', 'visible');
                }

            }
            else {
              
                var orderItem = {
                    ItemId: $('.item', this).val(),
                    CategoryId: $('.pc', this).val(),
                    SubCategoryId: $('.sb', this).val(),
                    SizeId: $('.size', this).val(),
                    Quantity: parseInt($('.quantity', this).val()),
                    OrderValue: parseInt($('.OrderValue', this).val()),

                }

                list.push(orderItem);

            }
        })

        if (errorItemCount > 0) {
            $('#orderItemError').text(errorItemCount + " invalid entry in order item list.");
            isAllValid = false;
        }

        if (list.length == 0) {

            $('#orderItemError').text('At least 1 order item required.');

            isAllValid = false;
        }

        if (!($('#totalOrderValue').val().trim() != '' && (parseInt($('#totalOrderValue').val()) || 0))) {

            isAllValid = false;
            $('#totalOrderValue').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#totalOrderValue').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#orderDate').text == '') {
            $('#orderDate').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#orderDate').siblings('span.error').css('visibility', 'hidden');
        }

        if (isAllValid == true) {

            var data = {
                CustomerID: $('#CustomerList').val(),
                Paymentmode: $('#Paymentmode').val(),
                TotalOrderValue: parseInt($('#totalOrderValue').val()),

                OrderDetails: list,
            }


            $.ajax({
                type: 'POST',
                url: '/SalesOrder/Save',

                data: JSON.stringify(data),
                contentType: 'application/json',
                success: function (data) {
                    if (data) {
                        $('#orderItemSuccess').text('New sales order created.');
                        $('#orderNo').text(data);
                        visitLogData();
                    }
                    $('#submit').text('Save');
                },
                error: function (error) {
                    alert("Something went wrong!")
                    $('#submit').text('Save');
                }
            });
        }

    });

});

const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const Datetime = urlParams.get('Datetime');
const Customerid = urlParams.get('Customerid');
const Amount = urlParams.get('Amount');
const Paymentmode = urlParams.get('Paymentmode');
var visitLogStatus = localStorage.getItem('visitLogCreate');


function visitLogData() {

    if (visitLogStatus == "true") {
        localStorage.setItem('visitLogCreate', "");
        localStorage.setItem('visitLogCreateData', "VisitData");
        window.location.href = "/VisitLog/Create?Datetime=" + Datetime + "&Customerid=" + Customerid + "&Paymentmode=" + Paymentmode + "&Amount=" + Amount;
    }
    else {
        location.reload();
    }
}


LoadCustomerList($('#CustomerList'));
LoadproductCategory($('#productCategory'));





