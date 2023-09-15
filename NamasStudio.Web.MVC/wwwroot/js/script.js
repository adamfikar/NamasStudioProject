//Auth Setting 
function getCookie(name) {
    let value = "; " + document.cookie;
    let parts = value.split("; " + name + "=");
    if (parts.length == 2) return parts.pop().split(";").shift();
}

let dataToken = getCookie("token");

function deleteCookie(cookieName) {
    document.cookie = cookieName + "=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
}

$("#logoutBtn").click(function (e) {
   // e.preventDefault();
    console.log("a");
    deleteCookie('token');
});


//Masonry Layout
document.addEventListener('DOMContentLoaded', function () {
    let container = document.querySelector('#masonry-container');
    let masonry = new Masonry(container, {
        itemSelector: '.masonry-card',
        columnWidth: '.masonry-card',
        gutter: 20 // Adjust the gutter (gap between items) as needed
    });
});

// --> CATEGORY PART


$('#myModalCategory').on('click', function () {
    $("#categoryName").val("");
    $("categoryDesc").val("");
    $('.modal-category').modal('show');
});

$(".btn-category-submit").click(function (event) {
    event.preventDefault();
    const categoryName = $("#categoryName").val();
    const categoryDesc = $("#categoryDesc").val();
    let isError = false;

    const dto = {
        CategoryName: categoryName,
        Description: categoryDesc
    };

    $.ajax({
        method: 'POST',
        url: "https://localhost:7231/api/CategoryProduct/Insert",
        headers: {
            'Authorization': 'Bearer ' + dataToken // Set the authorization header correctly
        },
        data: JSON.stringify(dto),
        contentType: 'application/json',
        beforeSend: function () {
            // Disable the button before sending the request
            $(".btn-category-submit").prop('disabled', true).html('Saving...');
        },
        success: function (response) {
            // Handle successful response from the server
            setTimeout(function () {
                const alertPlaceholder = $('#liveAlertPlaceholder');
                const message = 'Data saved successfully.';
                const type = 'success';
                const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                const content = $('<div>').text(message);
                const closeBtn = $('<button>').addClass('btn-close').attr({
                    type: 'button',
                    'data-bs-dismiss': 'alert',
                    'aria-label': 'Close'
                });
                wrapper.append(content, closeBtn);
                alertPlaceholder.append(wrapper);

                setTimeout(() => {
                    wrapper.alert('close');
                    setTimeout(() => {
                        location.reload();
                    }, 500);
                }, 2000);
            }, 1000);
        },
        error: function (xhr, status, error) {
            // Handle server-side validation errors
            if (xhr.status === 400) {
                isError = true;
                const errorObj = JSON.parse(xhr.responseText);
                const alertPlaceholder = $('#liveAlertPlaceholder');
                const errors = errorObj.errors;

                $.each(errors, function (field, errorMessages) {
                    const message = errorMessages.join('<br> ');
                    const type = 'danger';
                    const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                    const content = $('<div>').html(message);
                    const closeBtn = $('<button>').addClass('btn-close').attr({
                        type: 'button',
                        'data-bs-dismiss': 'alert',
                        'aria-label': 'Close'
                    });
                    wrapper.append(content, closeBtn);
                    alertPlaceholder.append(wrapper);

                    setTimeout(() => {
                        wrapper.alert('close');
                    }, 3000);
                });

            } else {
                console.log(xhr.responseText);
            }
        },
        complete: function () {
            if (isError) {
                setTimeout(function () { $(".btn-category-submit").prop('disabled', false).html('Delete') }, 1000);
            }
        }
    });
});


$(".delete-category").click(function (event) {
    const id = $(this).data("id");
    $(".btn-delete-category").data("id", id);
    $('#deleteModal').modal('show');
});

$(".btn-delete-category").click(function (event) {
    const id = $(this).data("id");
    const url = `https://localhost:7231/api/CategoryProduct/Delete?categoryId=${id}`;

    let isError = false;

    $.ajax({
        method: 'DELETE',
        url: url,
        headers: {
            'Authorization': 'Bearer ' + dataToken // Set the authorization header correctly
        },
        contentType: 'application/json',
        beforeSend: function () {
            // Disable the button before sending the request
            $(".exit-delete").css('display', 'none');
            $(".btn-cancel-delete").css('display', 'none');
            $(".btn-delete-category").prop('disabled', true).html('Deleting...');
        },
        success: function (response) {
            setTimeout(function () {
                const alertPlaceholder = $('#alertDelete');
                const message = 'Data deleted successfully.';
                const type = 'success';
                const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                const content = $('<div>').text(message);
                const closeBtn = $('<button>').addClass('btn-close').attr({
                    type: 'button',
                    'data-bs-dismiss': 'alert',
                    'aria-label': 'Close'
                });
                wrapper.append(content, closeBtn);
                alertPlaceholder.append(wrapper);

                setTimeout(() => {
                    wrapper.alert('close');
                    setTimeout(() => {
                        location.reload();
                    }, 500);
                }, 2000);
            }, 1000);
        },
        error: function (xhr, status, error) {
            if (xhr.status === 400) {
                isError = true;
                const errorObj = xhr.responseText;
                const alertPlaceholder = $('#alertDelete');
                const message = errorObj;
                const type = 'danger';
                const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                const content = $('<div>').html(message);
                const closeBtn = $('<button>').addClass('btn-close').attr({
                    type: 'button',
                    'data-bs-dismiss': 'alert',
                    'aria-label': 'Close'
                });
                wrapper.append(content, closeBtn);
                alertPlaceholder.append(wrapper);

                setTimeout(() => {
                    wrapper.alert('close');
                }, 3000);


            } else {
                console.log(xhr.responseText);
            }
        },
        complete: function () {
            if (isError) {
                setTimeout(function () { $(".btn-delete-category").prop('disabled', false).html('Submit') }, 1000);
            }
        }
    });
});

$(".update-category").click(function (event) {
    $("#CategoryName").prop('readonly', true);
    const id = $(this).data("id");
    const url = `https://localhost:7231/api/CategoryProduct/GetCategory?categoryId=${id}`;
    console.log(id);
    const modal = $(".modal-update-category");
    modal.modal('show');

    $.ajax({
        method: 'GET',
        url: url,
        headers: {
            'Authorization': 'Bearer ' + dataToken // Set the authorization header correctly
        },
        contentType: 'application/json',
        success: function (response) {
            console.log(response);
            modal.find("#categoryId").val(response.categoryId);
            modal.find("#CategoryName").val(response.categoryName);
            modal.find("#Description").val(response.description);

        },
        error: function (xhr, status, error) {
            console.log(`Error: ${error}`);
        }
    });
});

$(".btn-submit-update-category").click(function (event) {
    // Prevent the default form submission behavior
    event.preventDefault();

    // Get the data from the input fields
    let dto = {
        CategoryId: $("#categoryId").val(),
        categoryName: $("#CategoryName").val(),
        description: $("#Description").val()
    };

    console.log(dto);

    $.ajax({
        method: 'PUT',
        url: 'https://localhost:7231/api/CategoryProduct/Update',
        headers: {
            'Authorization': 'Bearer ' + dataToken // Set the authorization header correctly
        },
        contentType: 'application/json',
        data: JSON.stringify(dto),
        beforeSend: function () {
            // Disable the button before sending the request
            $(".btn-submit-update-category").prop('disabled', true).html('Updating...');
        },
        success: function (response) {
            setTimeout(function () {
                const alertPlaceholder = $('#alert');
                const message = 'Data updated successfully.';
                const type = 'success';
                const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                const content = $('<div>').text(message);
                const closeBtn = $('<button>').addClass('btn-close').attr({
                    type: 'button',
                    'data-bs-dismiss': 'alert',
                    'aria-label': 'Close'
                });
                wrapper.append(content, closeBtn);
                alertPlaceholder.append(wrapper);

                setTimeout(() => {
                    wrapper.alert('close');
                    setTimeout(() => {
                        location.reload();
                    }, 500);
                }, 2000);
            }, 1000);
        },
        error: function (xhr, textStatus, errorThrown) {
            // Handle error if the server returns an error
            console.error(xhr.responseText);
        }
    });
});



// --> PRODUCT CODE
$('#myModalProduct').on('click', function () {
    $("#productName").val("");
    $("#productDesc").val("");
    $("#productCategory").val("");
    $("#productPrice").val("");
    $("#productWeight").val("");
    $("#productFabric").val("");
    $("#productColor").val("");
    $('.modal-product').modal('show');
});

$(".btn-product-submit").click(function (event) {
    event.preventDefault();
    const categoryId = $("#productCategory").val() == "" ? 0 : $("#productCategory").val();
    const productName = $("#productName").val();
    const unitPrice = $("#productPrice").val() == "" ? 0 : $("#productPrice").val();
    const description = $("#productDesc").val();
    const weight = $("#productWeight").val() == "" ? 0 : $("#productWeight").val();
    const fabric = $("#productFabric").val();
    const color = $("#productColor").val();

    let isError = false;

    const dto = {
        CategoryId: categoryId,
        ProductName: productName,
        UnitPrice: unitPrice,
        Description: description,
        Weight: weight,
        Fabric: fabric,
        Color: color
    };

    console.log(dto);

    $.ajax({
        method: 'POST',
        url: "https://localhost:7231/api/ProductsApi/Insert",
        headers: {
            'Authorization': 'Bearer ' + dataToken // Set the authorization header correctly
        },
        data: JSON.stringify(dto),
        contentType: 'application/json',
        beforeSend: function () {
            // Disable the button before sending the request
            $(".btn-product-submit").prop('disabled', true).html('Saving...');
        },
        success: function (response) {
            // Handle successful response from the server
            setTimeout(function () {
                const alertPlaceholder = $('#liveAlertPlaceholder');
                const message = 'Data saved successfully.';
                const type = 'success';
                const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                const content = $('<div>').text(message);
                const closeBtn = $('<button>').addClass('btn-close').attr({
                    type: 'button',
                    'data-bs-dismiss': 'alert',
                    'aria-label': 'Close'
                });
                wrapper.append(content, closeBtn);
                alertPlaceholder.append(wrapper);

                setTimeout(() => {
                    wrapper.alert('close');
                    setTimeout(() => {
                        location.reload();
                    }, 500);
                }, 2000);
            }, 1000);
        },
        error: function (xhr, status, error) {
            // Handle server-side validation errors
            if (xhr.status === 400) {
                isError = true;
                const errorObj = JSON.parse(xhr.responseText);
                const alertPlaceholder = $('#liveAlertPlaceholder');
                const errors = errorObj.errors;
                let messages = [];

                $.each(errors, function (field, errorMessages) {
                    messages = errorMessages;

                });

                let message = messages.join('<br>');
                const type = 'danger';
                const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                const content = $('<div>').html(message);
                const closeBtn = $('<button>').addClass('btn-close').attr({
                    type: 'button',
                    'data-bs-dismiss': 'alert',
                    'aria-label': 'Close'
                });
                wrapper.append(content, closeBtn);
                alertPlaceholder.append(wrapper);

                wrapper.css("z-index", 9999);

                setTimeout(() => {
                    wrapper.alert('close');
                }, 3000);

            } else {
                console.log(xhr.responseText);
            }
        },
        complete: function () {
            if (isError) {
                setTimeout(function () { $(".btn-product-submit").prop('disabled', false).html('Submit') }, 1000);
            }
        }
    });
});

$(".delete-product").click(function (event) {
    const id = $(this).data("id");
    console.log(id);
    $(".btn-delete-product").data("id", id);
    $('#deleteProductModal').modal('show');
});

$(".btn-delete-product").click(function (event) {
    const id = $(this).data("id");
    const url = `https://localhost:7231/api/ProductsApi/Delete?productId=${id}`;
    $.ajax({
        method: 'DELETE',
        url: url,
        headers: {
            'Authorization': 'Bearer ' + dataToken // Set the authorization header correctly
        },
        contentType: 'application/json',
        beforeSend: function () {
            // Disable the button before sending the request
            $(".exit-delete").css('display', 'none');
            $(".btn-cancel-delete").css('display', 'none');
            $(".btn-delete-product").prop('disabled', true).html('Deleting...');
        },
        success: function (response) {
            setTimeout(function () {
                const alertPlaceholder = $('#alertDelete');
                const message = 'Data deleted successfully.';
                const type = 'success';
                const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                const content = $('<div>').text(message);
                const closeBtn = $('<button>').addClass('btn-close').attr({
                    type: 'button',
                    'data-bs-dismiss': 'alert',
                    'aria-label': 'Close'
                });
                wrapper.append(content, closeBtn);
                alertPlaceholder.append(wrapper);

                setTimeout(() => {
                    wrapper.alert('close');
                    setTimeout(() => {
                        location.reload();
                    }, 500);
                }, 2000);
            }, 1000);
        },
        error: function (xhr, status, error) {
            console.log(xhr.responseText);
        }
    });
});

$(".update-product").click(function (event) {
    $("#productNameU").prop('readonly', true);
    const id = $(this).data("id");
    const url = `https://localhost:7231/api/ProductsApi/GetProductById?productId=${id}`;
    const modal = $(".modal-update-product");
    modal.modal('show');

    $.ajax({
        method: 'GET',
        url: url,
        headers: {
            'Authorization': 'Bearer ' + dataToken // Set the authorization header correctly
        },
        contentType: 'application/json',
        success: function (response) {
            console.log(response);
            modal.find("#productIdU").val(response.productId);
            modal.find("#productCategoryU").val(response.categoryId);
            modal.find("#productNameU").val(response.productName);
            modal.find("#productPriceU").val(response.unitPrice);
            modal.find("#productDescU").val(response.description);
            modal.find("#productWeightU").val(response.weight);
            modal.find("#productFabricU").val(response.fabric);
            modal.find("#productColorU").val(response.color);
        },
        error: function (xhr, status, error) {
            console.log(`Error: ${error}`);
        }
    });
});


$(".btn-submit-update-product").click(function (event) {
    // Prevent the default form submission behavior
    //event.preventDefault();

    isError = false;
    // Get the data from the input fields
    let dto = {
        productId: $("#productIdU").val(),
        CategoryId: $("#productCategoryU").val() === "" ? 0 : $("#productCategoryU").val(),
        productName: $("#productNameU").val(),
        UnitPrice: $("#productPriceU").val() === "" ? 0 : $("#productPriceU").val(),
        Color: $("#productColorU").val(),
        Fabric: $("#productFabricU").val() === "" ? 0 : $("#productFabricU").val(),
        Description: $("#productDescU").val(),
        Weight: $("#productWeightU").val() === "" ? 0 : $("#productWeightU").val(),
    };

    console.log(dto);

    $.ajax({
        method: 'PUT',
        url: 'https://localhost:7231/api/ProductsApi/Update',
        headers: {
            'Authorization': 'Bearer ' + dataToken // Set the authorization header correctly
        },
        contentType: 'application/json',
        data: JSON.stringify(dto),
        beforeSend: function () {
            // Disable the button before sending the request
            $(".btn-submit-update-product").prop('disabled', true).html('Updating...');
        },
        success: function (response) {
            setTimeout(function () {
                const alertPlaceholder = $('#alert');
                const message = 'Data updated successfully.';
                const type = 'success';
                const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                const content = $('<div>').text(message);
                const closeBtn = $('<button>').addClass('btn-close').attr({
                    type: 'button',
                    'data-bs-dismiss': 'alert',
                    'aria-label': 'Close'
                });
                wrapper.append(content, closeBtn);
                alertPlaceholder.append(wrapper);

                wrapper.css("z-index", 9999);

                setTimeout(() => {
                    wrapper.alert('close');
                    setTimeout(() => {
                        location.reload();
                    }, 500);
                }, 2000);
            }, 1000);
        },
        error: function (xhr, textStatus, errorThrown) {
            // Handle error if the server returns an error
            if (xhr.status === 400) {
                isError = true;
                const errorObj = JSON.parse(xhr.responseText);
                const alertPlaceholder = $('#alert');
                const errors = errorObj.errors;
                let messages = [];

                $.each(errors, function (field, errorMessages) {
                    messages = errorMessages;

                });

                let message = messages.join('<br>');
                const type = 'danger';
                const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                const content = $('<div>').html(message);
                const closeBtn = $('<button>').addClass('btn-close').attr({
                    type: 'button',
                    'data-bs-dismiss': 'alert',
                    'aria-label': 'Close'
                });
                wrapper.append(content, closeBtn);
                alertPlaceholder.append(wrapper);

                wrapper.css("z-index", 9999);

                setTimeout(() => {
                    wrapper.alert('close');
                }, 3000);

            } else {
                console.log(xhr.responseText);
            }
        },
        complete: function () {
            if (isError) {
                setTimeout(function () { $(".btn-submit-update-product").prop('disabled', false).html('Submit') }, 1000);
            }
        }
    });
});


// PRODUCT STOCK
$('#myModalProductStock').on('click', function () {
    //const id = $(this).data("id");
    const categoryId = $("#categoryId").val();
    console.log(categoryId);
    $("#sizeName").val("");
    $("#stock").val("");

    const select = $("#sizeName");
    select.empty();
    const defaultValSelect = $('<option>').val("").text("Select Size");
    select.append(defaultValSelect);
    $.ajax({
        method: 'GET',
        url: `https://localhost:7231/api/ProductStockApi/GetSizeDropdown?categoryId=${categoryId}`,
        headers: {
            'Authorization': 'Bearer ' + dataToken // Set the authorization header correctly
        },
        contentType: 'application/json',
        success: function (response) {

            $.each(response, function (value, text) {
                console.log(text);
                let option = $('<option>').val(text.value).text(text.text);
                select.append(option);
            });
            //modal.find("#productIdU").val(response.productId);
        },
        error: function (xhr, status, error) {
            console.log(xhr.responseText);
        },
    });

    $('.modal-productStock').modal('show');
});

$(".btn-productStock-submit").click(function (event) {
    event.preventDefault();
    const productId = $("#productId").val();
    const sizeId = $("#sizeName").val() == "" ? 0 : $("#sizeName").val();
    const stock = $("#stock").val() == "" ? 0 : $("#stock").val();

    let isError = false;

    const dto = {
        ProductId: productId,
        SizeId: sizeId,
        Stock: stock
    };

    console.log(dto);

    $.ajax({
        method: 'POST',
        url: "https://localhost:7231/api/ProductStockApi/Insert",
        headers: {
            'Authorization': 'Bearer ' + dataToken // Set the authorization header correctly
        },
        data: JSON.stringify(dto),
        contentType: 'application/json',
        beforeSend: function () {
            // Disable the button before sending the request
            $(".btn-productStock-submit").prop('disabled', true).html('Saving...');
        },
        success: function (response) {
            // Handle successful response from the server
            setTimeout(function () {
                const alertPlaceholder = $('#liveAlertPlaceholder');
                const message = 'Data saved successfully.';
                const type = 'success';
                const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                const content = $('<div>').text(message);
                const closeBtn = $('<button>').addClass('btn-close').attr({
                    type: 'button',
                    'data-bs-dismiss': 'alert',
                    'aria-label': 'Close'
                });
                wrapper.append(content, closeBtn);
                alertPlaceholder.append(wrapper);

                setTimeout(() => {
                    wrapper.alert('close');
                    setTimeout(() => {
                        location.reload();
                    }, 500);
                }, 2000);
            }, 1000);
        },
        error: function (xhr, status, error) {
            // Handle server-side validation errors
            if (xhr.status === 400) {
                isError = true;
                const errorObj = JSON.parse(xhr.responseText);
                const alertPlaceholder = $('#liveAlertPlaceholder');
                const errors = errorObj.errors;
                let messages = [];

                $.each(errors, function (field, errorMessages) {
                    messages = errorMessages;

                });

                let message = messages.join('<br>');
                const type = 'danger';
                const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                const content = $('<div>').html(message);
                const closeBtn = $('<button>').addClass('btn-close').attr({
                    type: 'button',
                    'data-bs-dismiss': 'alert',
                    'aria-label': 'Close'
                });
                wrapper.append(content, closeBtn);
                alertPlaceholder.append(wrapper);

                wrapper.css("z-index", 9999);

                setTimeout(() => {
                    wrapper.alert('close');
                }, 3000);

            }
            else if (xhr.status === 500) {
                isError = true;
            }
            else {
                console.log(xhr.responseText);
            }
        },
        complete: function () {
            if (isError) {
                setTimeout(function () { $(".btn-productStock-submit").prop('disabled', false).html('Submit') }, 1000);
            }
        }
    });
});


$(".delete-productStock").click(function (event) {
    const element = $(this).data("id");
    const splitData = element.split(" ");

    $(".btn-delete-productStock").data("myData", splitData);
    $('#deleteProductStockModal').modal('show');
});

$(".btn-delete-productStock").click(function (event) {
    const id = $(this).data("myData");
    console.log(id);
    const url = `https://localhost:7231/api/ProductStockApi/Delete?productId=${id[0]}&sizeId=${id[1]}`;
    $.ajax({
        method: 'DELETE',
        url: url,
        headers: {
            'Authorization': 'Bearer ' + dataToken // Set the authorization header correctly
        },
        contentType: 'application/json',
        beforeSend: function () {
            // Disable the button before sending the request
            $(".exit-delete").css('display', 'none');
            $(".btn-cancel-delete").css('display', 'none');
            $(".btn-delete-productStock").prop('disabled', true).html('Deleting...');
        },
        success: function (response) {
            setTimeout(function () {
                const alertPlaceholder = $('#alertDelete');
                const message = 'Data deleted successfully.';
                const type = 'success';
                const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                const content = $('<div>').text(message);
                const closeBtn = $('<button>').addClass('btn-close').attr({
                    type: 'button',
                    'data-bs-dismiss': 'alert',
                    'aria-label': 'Close'
                });
                wrapper.append(content, closeBtn);
                alertPlaceholder.append(wrapper);

                setTimeout(() => {
                    wrapper.alert('close');
                    setTimeout(() => {
                        location.reload();
                    }, 500);
                }, 2000);
            }, 1000);
        },
        error: function (xhr, status, error) {
            console.log(xhr.responseText);
        }
    });
});

$(".update-productStock").click(function (event) {
    const id = $(this).data("id");
    const splitData = id.split(" ");
    console.log(splitData);
    const url = `https://localhost:7231/api/ProductStockApi/GetProductStockById?productId=${splitData[0]}&sizeId=${splitData[1]}`;
    const modal = $(".modal-update-productStock");
    modal.modal('show');

    $.ajax({
        method: 'GET',
        url: url,
        headers: {
            'Authorization': 'Bearer ' + dataToken // Set the authorization header correctly
        },
        contentType: 'application/json',
        success: function (response) {
            console.log(response);
            modal.find("#productIdU").val(response.productId);
            modal.find("#sizeNameU").val(response.sizeName);
            modal.find("#sizeIdU").val(response.sizeId);
            modal.find("#stockU").val(response.stock);
        },
        error: function (xhr, status, error) {
            console.log(`Error: ${error}`);
        }
    });
});


$(".btn-submit-update-productStock").click(function (event) {
    // Prevent the default form submission behavior
    event.preventDefault();

    isError = false;
    // Get the data from the input fields
    let dto = {
        productId: $("#productIdU").val(),
        sizeId: $("#sizeIdU").val() === "" ? 0 : $("#sizeIdU").val(),
        stock: $("#stockU").val() === "" ? 0 : $("#stockU").val(),
    };

    console.log(dto);

    $.ajax({
        method: 'PUT',
        url: 'https://localhost:7231/api/ProductStockApi/Update',
        headers: {
            'Authorization': 'Bearer ' + dataToken // Set the authorization header correctly
        },
        contentType: 'application/json',
        data: JSON.stringify(dto),
        beforeSend: function () {
            // Disable the button before sending the request
            $(".btn-submit-update-productStock").prop('disabled', true).html('Updating...');
        },
        success: function (response) {
            setTimeout(function () {
                const alertPlaceholder = $('#alert');
                const message = 'Data updated successfully.';
                const type = 'success';
                const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                const content = $('<div>').text(message);
                const closeBtn = $('<button>').addClass('btn-close').attr({
                    type: 'button',
                    'data-bs-dismiss': 'alert',
                    'aria-label': 'Close'
                });
                wrapper.append(content, closeBtn);
                alertPlaceholder.append(wrapper);

                wrapper.css("z-index", 9999);

                setTimeout(() => {
                    wrapper.alert('close');
                    setTimeout(() => {
                        location.reload();
                    }, 500);
                }, 2000);
            }, 1000);
        },
        error: function (xhr, textStatus, errorThrown) {
            // Handle error if the server returns an error
            if (xhr.status === 400) {
                isError = true;
                const errorObj = JSON.parse(xhr.responseText);
                const alertPlaceholder = $('#alert');
                const errors = errorObj.errors;
                let messages = [];

                $.each(errors, function (field, errorMessages) {
                    messages = errorMessages;

                });

                let message = messages.join('<br>');
                const type = 'danger';
                const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                const content = $('<div>').html(message);
                const closeBtn = $('<button>').addClass('btn-close').attr({
                    type: 'button',
                    'data-bs-dismiss': 'alert',
                    'aria-label': 'Close'
                });
                wrapper.append(content, closeBtn);
                alertPlaceholder.append(wrapper);

                wrapper.css("z-index", 9999);

                setTimeout(() => {
                    wrapper.alert('close');
                }, 3000);

            } else {
                console.log(xhr.responseText);
            }
        },
        complete: function () {
            if (isError) {
                setTimeout(function () { $(".btn-submit-update-productStock").prop('disabled', false).html('Submit') }, 1000);
            }
        }
    });
});

//Product Size
$('#myModalProductSize').on('click', function () {
    $("#sizeName").val("");
    $("#waist").val("");
    $("hips").val("");
    $("#lengthLower").val("");
    $("#bust").val("");
    $("#lengthUpper").val("");
    $("#armHole").val("");
    $("#bottomSleeve").val("");
    $("#sleeveLength").val("");
    $("#desc").val("");

    const select = $("#category");
    select.empty();
    const defaultValSelect = $('<option>').val("").text("Select Category");
    select.append(defaultValSelect);
    $.ajax({
        method: 'GET',
        url: 'https://localhost:7231/api/ProductSizeApi/GetCategoryDropdown',
        headers: {
            'Authorization': 'Bearer ' + dataToken // Set the authorization header correctly
        },
        contentType: 'application/json',
        success: function (response) {

            $.each(response, function (value, text) {

                let option = $('<option>').val(text.value).text(text.text);
                select.append(option);
            });
            //modal.find("#productIdU").val(response.productId);
        },
        error: function (xhr, status, error) {
            console.log(xhr.responseText);
        },
    });

    $('.modal-productSize').modal('show');
});

$(".btn-productSize-submit").click(function (event) {
    event.preventDefault();
    const sizeName = $("#sizeName").val();
    const category = $("#category").val() == "" ? 0 : $("#category").val();
    const waist = $("#waist").val();
    const hips = $("#hips").val();
    const lengthLower = $("#lengthLower").val();
    const bust = $("#bust").val();
    const lengthUpper = $("#lengthUpper").val();
    const armHole = $("#armHole").val();
    const bottomSleeve = $("#bottomSleeve").val();
    const sleeveLength = $("#sleeveLength").val();
    const desc = $("#desc").val();

    let isError = false;

    const dto = {
        sizeName: sizeName,
        categoryId: category,
        waist: waist,
        hips: hips,
        lengthLower: lengthLower,
        bust: bust,
        lengthUpper: lengthUpper,
        armHole: armHole,
        bottomSleeve: bottomSleeve,
        sleeveLength: sleeveLength,
        desc: desc
    };

    console.log(dto);

    $.ajax({
        method: 'POST',
        url: "https://localhost:7231/api/ProductSizeApi/Insert",
        headers: {
            'Authorization': 'Bearer ' + dataToken // Set the authorization header correctly
        },
        data: JSON.stringify(dto),
        contentType: 'application/json',
        beforeSend: function () {
            // Disable the button before sending the request
            $(".btn-productSize-submit").prop('disabled', true).html('Saving...');
        },
        success: function (response) {
            // Handle successful response from the server
            setTimeout(function () {
                const alertPlaceholder = $('#liveAlertPlaceholder');
                const message = 'Data saved successfully.';
                const type = 'success';
                const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                const content = $('<div>').text(message);
                const closeBtn = $('<button>').addClass('btn-close').attr({
                    type: 'button',
                    'data-bs-dismiss': 'alert',
                    'aria-label': 'Close'
                });
                wrapper.append(content, closeBtn);
                alertPlaceholder.append(wrapper);

                setTimeout(() => {
                    wrapper.alert('close');
                    setTimeout(() => {
                        location.reload();
                    }, 500);
                }, 2000);
            }, 1000);
        },
        error: function (xhr, status, error) {
            // Handle server-side validation errors
            if (xhr.status === 400) {
                isError = true;
                const errorObj = JSON.parse(xhr.responseText);
                const alertPlaceholder = $('#liveAlertPlaceholder');
                const errors = errorObj.errors;
                let messages = [];

                $.each(errors, function (field, errorMessages) {
                    messages = errorMessages;

                });

                let message = messages.join('<br>');
                const type = 'danger';
                const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                const content = $('<div>').html(message);
                const closeBtn = $('<button>').addClass('btn-close').attr({
                    type: 'button',
                    'data-bs-dismiss': 'alert',
                    'aria-label': 'Close'
                });
                wrapper.append(content, closeBtn);
                alertPlaceholder.append(wrapper);

                wrapper.css("z-index", 9999);

                setTimeout(() => {
                    wrapper.alert('close');
                }, 3000);

            }
            else if (xhr.status === 500) {
                isError = true;
            }
            else {
                console.log(xhr.responseText);
            }
        },
        complete: function () {
            if (isError) {
                setTimeout(function () { $(".btn-productSize-submit").prop('disabled', false).html('Submit') }, 1000);
            }
        }
    });

});

$(".delete-productSizeD").click(function (event) {
    const element = $(this).data("id");
    const splitData = element.split(" ");
    $(".btn-delete-productSize").data("myData", splitData);
    $("#deleteProductSizeModal").modal('show');
});


$(".btn-delete-productSize").click(function () {
    const id = $(this).data("myData");
    console.log(id);

    const url = `https://localhost:7231/api/ProductSizeApi/Delete?sizeId=${id[0]}&categoryId=${id[1]}`;
    $.ajax({
        method: 'DELETE',
        url: url,
        headers: {
            'Authorization': 'Bearer ' + dataToken // Set the authorization header correctly
        },
        contentType: 'application/json',
        beforeSend: function () {
            // Disable the button before sending the request
            $(".exit-delete").css('display', 'none');
            $(".btn-cancel-delete").css('display', 'none');
            $(".btn-delete-productSize").prop('disabled', true).html('Deleting...');
        },
        success: function (response) {
            setTimeout(function () {
                const alertPlaceholder = $('#alertDelete');
                const message = 'Data deleted successfully.';
                const type = 'success';
                const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                const content = $('<div>').text(message);
                const closeBtn = $('<button>').addClass('btn-close').attr({
                    type: 'button',
                    'data-bs-dismiss': 'alert',
                    'aria-label': 'Close'
                });
                wrapper.append(content, closeBtn);
                alertPlaceholder.append(wrapper);

                setTimeout(() => {
                    wrapper.alert('close');
                    setTimeout(() => {
                        location.reload();
                    }, 500);
                }, 2000);
            }, 1000);
        },
        error: function (xhr, status, error) {
            if (xhr.status === 400) {
                isError = true;
                const errorObj = xhr.responseText;
                const alertPlaceholder = $('#alertDelete');
                const message = errorObj;
                const type = 'danger';
                const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                const content = $('<div>').html(message);
                const closeBtn = $('<button>').addClass('btn-close').attr({
                    type: 'button',
                    'data-bs-dismiss': 'alert',
                    'aria-label': 'Close'
                });
                wrapper.append(content, closeBtn);
                alertPlaceholder.append(wrapper);

                setTimeout(() => {
                    wrapper.alert('close');
                }, 3000);


            } else {
                console.log(xhr.responseText);
            }
        },
        complete: function () {
            if (isError) {
                setTimeout(function () { $(".btn-delete-productSize").prop('disabled', false).html('Delete') }, 1000);
            }
        }
    });
});


$(".update-productSize").click(function (event) {
    const id = $(this).data("id");
    const splitData = id.split(" ");
    //console.log(splitData);
    const url = `https://localhost:7231/api/ProductSizeApi/GetProductSize?sizeId=${splitData[0]}`;
    const modal = $(".modal-update-productSize");
    modal.modal('show');

    $.ajax({
        method: 'GET',
        url: url,
        headers: {
            'Authorization': 'Bearer ' + dataToken // Set the authorization header correctly
        },
        contentType: 'application/json',
        success: function (response) {
            console.log(response);
            modal.find("#sizeNameU").val(response.sizeName);
            modal.find("#sizeIdU").val(response.sizeId);
            modal.find("#categoryIdU").val(splitData[1]);
            modal.find("#waistU").val(response.waist);
            modal.find("#hipsU").val(response.hips);
            modal.find("#lengthLowerU").val(response.lengthLower);
            modal.find("#bustU").val(response.bust);
            modal.find("#lengthUpperU").val(response.lengthUpper);
            modal.find("#armHoleU").val(response.armHole);
            modal.find("#bottomSleeveU").val(response.bottomSleeve);
            modal.find("#sleeveLengthU").val(response.sleeveLength);
            modal.find("#descU").val(response.desc);
        },
        error: function (xhr, status, error) {
            console.log(`Error: ${error}`);
        }
    });

});

$(".btn-productSizeUpdate-submit").click(function (event) {
    // Prevent the default form submission behavior
   // event.preventDefault();

    isError = false;
    // Get the data from the input fields
    let dto = {
        sizeId: $("#sizeIdU").val(),
        categoryId: $("#categoryIdU").val(),
        waist: $("#waistU").val(),
        hips: $("#hipsU").val(),
        lengthLower: $("#lengthLowerU").val(),
        bust: $("#bustU").val(),
        lengthUpper: $("#lengthUpperU").val(),
        armHole: $("#armHoleU").val(),
        bottomSleeve: $("#bottomSleeveU").val(),
        sleeveLength: $("#sleeveLengthU").val(),
        desc: $("#descU").val(),
    };

    console.log(dto);

    $.ajax({
        method: 'PUT',
        url: 'https://localhost:7231/api/ProductSizeApi/Update',
        headers: {
            'Authorization': 'Bearer ' + dataToken // Set the authorization header correctly
        },
        contentType: 'application/json',
        data: JSON.stringify(dto),
        beforeSend: function () {
            // Disable the button before sending the request
            $(".btn-productSizeUpdate-submit").prop('disabled', true).html('Updating...');
        },
        success: function (response) {
            setTimeout(function () {
                const alertPlaceholder = $('#alertUpdate');
                const message = 'Data updated successfully.';
                const type = 'success';
                const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                const content = $('<div>').text(message);
                const closeBtn = $('<button>').addClass('btn-close').attr({
                    type: 'button',
                    'data-bs-dismiss': 'alert',
                    'aria-label': 'Close'
                });
                wrapper.append(content, closeBtn);
                alertPlaceholder.append(wrapper);

                wrapper.css("z-index", 9999);

                setTimeout(() => {
                    wrapper.alert('close');
                    setTimeout(() => {
                        location.reload();
                    }, 500);
                }, 2000);
            }, 1000);
        },
        error: function (xhr, textStatus, errorThrown) {
            // Handle error if the server returns an error
            if (xhr.status === 400) {
                isError = true;
                const errorObj = JSON.parse(xhr.responseText);
                const alertPlaceholder = $('#alertUpdate');
                const errors = errorObj.errors;
                let messages = [];

                $.each(errors, function (field, errorMessages) {
                    messages = errorMessages;

                });

                let message = messages.join('<br>');
                const type = 'danger';
                const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                const content = $('<div>').html(message);
                const closeBtn = $('<button>').addClass('btn-close').attr({
                    type: 'button',
                    'data-bs-dismiss': 'alert',
                    'aria-label': 'Close'
                });
                wrapper.append(content, closeBtn);
                alertPlaceholder.append(wrapper);

                wrapper.css("z-index", 9999);

                setTimeout(() => {
                    wrapper.alert('close');
                }, 3000);

            } else {
                console.log(xhr.responseText);
            }
        },
        complete: function () {
            if (isError) {
                setTimeout(function () { $(".btn-productSizeUpdate-submit").prop('disabled', false).html('Submit') }, 1000);
            }
        }
    });
});



//Photo Products
$('#myModalPhotoBtn').on('click', function () {
    $('.modal-photo').modal('show');
});

$(".btn-photo-submit").click(function (event) {
    event.preventDefault();
    var isError = false;

    let productId = $('#productId').val();
    var imgInput = $('#img')[0].files[0];

    var formData = new FormData();
    formData.append('img', imgInput);

    $.ajax({
        method: 'POST',
        url: 'https://localhost:7231/api/ProductPhotoApi/UploadPhoto?productId=' + productId,
        headers: {
            'Authorization': 'Bearer ' + dataToken // Set the authorization header correctly
        },
        data: formData,
        processData: false,
        contentType: false,
        beforeSend: function () {
            // Disable the button before sending the request
            $(".btn-photo-submit").prop('disabled', true).html('Saving...');
        },
        success: function (response) {
            // Handle successful response from the server
            setTimeout(function () {
                const alertPlaceholder = $('#liveAlertPlaceholder');
                const message = 'Data saved successfully.';
                const type = 'success';
                const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                const content = $('<div>').text(message);
                const closeBtn = $('<button>').addClass('btn-close').attr({
                    type: 'button',
                    'data-bs-dismiss': 'alert',
                    'aria-label': 'Close'
                });
                wrapper.append(content, closeBtn);
                alertPlaceholder.append(wrapper);

                setTimeout(() => {
                    wrapper.alert('close');
                    setTimeout(() => {
                        location.reload();
                    }, 500);
                }, 2000);
            }, 1000);
        },
        error: function (xhr, status, error) {
            // Handle server-side validation errors
            if (xhr.status === 400) {
                isError = true;
                let imgError = false;
                let errorObj = {};

                if (xhr.responseText) {
                    try {
                        errorObj = JSON.parse(xhr.responseText);
                        imgError = true;
                    } catch (e) {
                        errorObj = { errors: xhr.responseText };
                    }
                }

                console.log(errorObj);

                const alertPlaceholder = $('#liveAlertPlaceholder');
                let errorMessage = "";

                if (imgError) {
                    if (errorObj.errors && errorObj.errors.img) {
                        errorMessage = errorObj.errors.img[0];
                    }
                } else {
                    errorMessage = errorObj.errors;
                }

                if (errorMessage) {
                    const type = 'danger';
                    const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                    const content = $('<div>').text(errorMessage); // Use text() to prevent potential XSS issues.
                    const closeBtn = $('<button>').addClass('btn-close').attr({
                        type: 'button',
                        'data-bs-dismiss': 'alert',
                        'aria-label': 'Close'
                    });
                    wrapper.append(content, closeBtn);
                    alertPlaceholder.append(wrapper);

                    wrapper.css("z-index", 9999);

                    setTimeout(() => {
                        wrapper.alert('close');
                    }, 3000);
                } else {
                    console.log(xhr.responseText);
                }
            } else {
                console.log(xhr.responseText);
            }
        },


        complete: function () {
            if (isError) {
                setTimeout(function () { $(".btn-photo-submit").prop('disabled', false).html('Upload') }, 1000);
            }
        }
    });
});




$(".btn-delete-productPhoto").click(function () {
    const id = $(this).data("myData");

    let isError = false;
    console.log(id);

    const url = `https://localhost:7231/api/ProductPhotoApi/DeletePhoto?photoId=${id}`;
    $.ajax({
        method: 'DELETE',
        url: url,
        headers: {
            'Authorization': 'Bearer ' + dataToken // Set the authorization header correctly
        },
        contentType: 'application/json',
        beforeSend: function () {
            // Disable the button before sending the request
            $(".exit-delete").css('display', 'none');
            $(".btn-cancel-delete").css('display', 'none');
            $(".btn-delete-productPhoto").prop('disabled', true).html('Deleting...');
        },
        success: function (response) {
            setTimeout(function () {
                const alertPlaceholder = $('#alertDelete');
                const message = 'Data deleted successfully.';
                const type = 'success';
                const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                const content = $('<div>').text(message);
                const closeBtn = $('<button>').addClass('btn-close').attr({
                    type: 'button',
                    'data-bs-dismiss': 'alert',
                    'aria-label': 'Close'
                });
                wrapper.append(content, closeBtn);
                alertPlaceholder.append(wrapper);

                setTimeout(() => {
                    wrapper.alert('close');
                    setTimeout(() => {
                        location.reload();
                    }, 500);
                }, 2000);
            }, 1000);
        },
        error: function (xhr, status, error) {
            if (xhr.status === 400) {

                isError = true;
                const errorObj = xhr.responseText;

                const alertPlaceholder = $('#alertDelete');
                const message = errorObj;
                const type = 'danger';
                const wrapper = $('<div>').addClass(`alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 animate__animated animate__slideInUp`);
                const content = $('<div>').html(message);
                const closeBtn = $('<button>').addClass('btn-close').attr({
                    type: 'button',
                    'data-bs-dismiss': 'alert',
                    'aria-label': 'Close'
                });
                wrapper.append(content, closeBtn);
                alertPlaceholder.append(wrapper);

                setTimeout(() => {
                    wrapper.alert('close');
                }, 3000);


            } else {
                console.log(xhr.responseText);
            }
        },
        complete: function () {
            if (isError) {
                setTimeout(function () { $(".btn-delete-productPhoto").prop('disabled', false).html('Delete') }, 1000);
            }
        }
    });
});


$("#masonry-container").on("click", ".remove", function (event) {
    const id = $(this).data("id");
    $("#deleteProductPhotoModal").modal('show');
    $(".btn-delete-productPhoto").data("myData", id);

});

$("#masonry-container").on("click", "#previewBtn", function (event) {
    $(".modal-previewProduct").modal("show");
    let data = $(this).data("photo");
    let content = $(".modal-contentPreview");
    content.empty();

    // Create a div element
    let div = $("<div></div>");

    // Create an img element with the src attribute set to the data value
    let img = $(`<img src="${data}" />`);

    // Add a class to the img element
    img.addClass("set-height");

    // Append the img element to the div
    div.append(img);

    // Append the div to the content
    content.append(div);

});

$(document).ready(function () {
    let parent = $(".parentArea");
    let login = $(".loginArea");
    let register = $(".registerArea");


    $("#signUpLink").click(function () {
        console.log("signUp");
        $(".loginArea").toggleClass("animate__animated animate__slideOutRight");
        login.fadeOut();
        setTimeout(() => {
            register.fadeIn(300);
            register.css("display", "block");
            parent.append(register);
            $(".loginArea").removeClass("animate__animated animate__slideOutRight");
        }, 500);
        
    });

    
    $("#signInLink").click(function () {
        console.log("signIn");
        $(".registerArea").toggleClass("animate__animated animate__slideOutLeft");
        setTimeout(() => {
            login.fadeIn(300);
            register.css("display", "none");
            $(".registerArea").removeClass("animate__animated animate__slideOutLeft");
        }, 500);
    });
});













