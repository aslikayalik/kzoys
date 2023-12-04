// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


/**arama motoru*/
document.getElementById("search").addEventListener("input", function () {
    var searchTerm = this.value.trim().toLowerCase();
    var rows = document.querySelectorAll("tbody tr");

    rows.forEach(function (row) {
        var studentName = row.children[0].textContent.toLowerCase();
        var studentLastName = row.children[1].textContent.toLowerCase();

        if (studentName.includes(searchTerm) || studentLastName.includes(searchTerm)) {
            row.style.display = "table-row";
        } else {
            row.style.display = "none";
        }
    });
});

document.getElementById("searchButton").addEventListener("click", function () {
  
    event.preventDefault();
    resetRowDisplay();
});

function resetRowDisplay() {
    var rows = document.querySelectorAll("tbody tr");
    rows.forEach(function (row) {
        row.style.display = "table-row";
    });
}



function capitalizeFirstWord(input) {
    var words = input.value.split(' '); 
    for (var i = 0; i < words.length; i++) {
        if (words[i]) {
            words[i] = words[i].charAt(0).toUpperCase() + words[i].slice(1).toLowerCase();
        }
    }
    input.value = words.join(' '); 
}


function toUpperCase(input) {
    input.value = input.value.toUpperCase();
}

