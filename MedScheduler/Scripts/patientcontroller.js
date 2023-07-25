function filterDoctors() {
    var specialization = document.getElementById("specialization").value.toLowerCase();
    var specializationCards = document.getElementsByClassName("specialization-card");

    for (var i = 0; i < specializationCards.length; i++) {
        var card = specializationCards[i];
        var cardSpecialization = card.dataset.specialization.toLowerCase();

        if (cardSpecialization.includes(specialization)) {
            card.style.display = "block";
        } else {
            card.style.display = "none";
        }
    }
}

//document.addEventListener("DOMContentLoaded", function () {
  //  console.log("hello")
  /*  var displayicon = document.getElementById("displayicon");
    var displayDetails = document.getElementById("displayDetails");
    displayicon.addEventListener("click", function (event) {
        event.preventDefault();
        displayDetails.classList.toggle("displaydetailsClick")
    })*/
//})

/*function displayview() {
    console.log("hello")
    var displayicon = document.getElementById("displayicon");
    var displayDetails = document.getElementById("displayDetails");
    displayDetails.classList.toggle("displaydetailsClick")

}*/
/*document.addEventListener("DOMContentLoaded", function () {
    var detailsIcon = document.getElementById("detailsIcon");
    var detailsDiv = document.querySelector(".displaydetails");
    detailsIcon.addEventListener("click", function (e) {
        e.preventDefault();
        detailsDiv.classList.toggle("displaydetailsClick");
    });
});*/

/*function toggleDetails() {
    var detailsIcon = document.getElementById("detailsIcon");
    var detailsDiv = document.querySelector(".displaydetails");

    if (detailsIcon && detailsDiv) {
        detailsIcon.addEventListener("click", function (e) {
            e.preventDefault();
            detailsDiv.classList.toggle("displaydetailsClick");
        });
    }
}

document.addEventListener("DOMContentLoaded", toggleDetails);*/

