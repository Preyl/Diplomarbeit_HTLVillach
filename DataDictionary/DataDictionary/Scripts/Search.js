////Search für Selected Datentypen

//searchBoxMeineDatentypen = document.querySelector("#searchBoxMeineDatentypen");
//searchedMeineDatentypen = document.querySelector("#selectedDatentypen");

//searchBoxAlleDatentypen = document.querySelector("#searchBoxAlleDatentypen");
//searchedAlleDatentypen = document.querySelector("#alleDatentypen");

//searchBoxAlleAnwendungen = document.querySelector("#searchBoxAlleAnwendugen");
//searchedAlleAnwendungen = document.querySelector("#alleAnwendungen");

//searchBoxMeineAnwendugen = document.querySelector("#searchBoxMeineAnwendungen");
//searchedMeineAnwendungen = document.querySelector("#selectedAnwendungen");

//searchBoxMeineFelder = document.querySelector("#searchBoxMeineFelder");
//searchedMeineFelder = document.querySelector("#selectedFelder");

//searchBoxAlleFelder = document.querySelector("#searchBoxAlleFelder");
//searchedAlleFelder = document.querySelector("#alleFelder");

////var when = "keyup"; //You can change this to keydown, keypress or change

//searchBoxMeineDatentypen.addEventListener("keyup", function (a) {
//    var text = a.target.value;
//    var options = searchedMeineDatentypen.options;
//    for (var i = 0; i < options.length; i++) {
//        var option = options[i];
//        var optionText = option.text;
//        var lowerOptionText = optionText.toLowerCase();
//        var lowerText = text.toLowerCase();
//        var regex = new RegExp("^" + text, "i");
//        var match = optionText.match(regex);
//        var contains = lowerOptionText.indexOf(lowerText) != -1;
//        if (match || contains) {
//            option.selected = true;
//            return;
//        }
//        searchBoxMeineDatentypen.selectedIndex = 0;
//    }
//});

///*******************************************************/
////Search für Alle Datentypen

//searchBoxAlleDatentypen.addEventListener("keyup", function (b) {
//    var text = b.target.value;
//    var options = searchedAlleDatentypen.options;
//    for (var i = 0; i < options.length; i++) {
//        var option = options[i];
//        var optionText = option.text;
//        var lowerOptionText = optionText.toLowerCase();
//        var lowerText = text.toLowerCase();
//        var regex = new RegExp("^" + text, "i");
//        var match = optionText.match(regex);
//        var contains = lowerOptionText.indexOf(lowerText) != -1;
//        if (match || contains) {
//            option.selected = true;
//            return;
//        }
//        searchBoxAlleDatentypen.selectedIndex = 0;
//    }
//});
///*******************************************************/
////Search für Alle Anwendungen

//searchBoxAlleAnwendungen.addEventListener("keyup", function (c) {
//    var text = c.target.value;
//    var options = searchedAlleAnwendungen.options;
//    for (var i = 0; i < options.length; i++) {
//        var option = options[i];
//        var optionText = option.text;
//        var lowerOptionText = optionText.toLowerCase();
//        var lowerText = text.toLowerCase();
//        var regex = new RegExp("^" + text, "i");
//        var match = optionText.match(regex);
//        var contains = lowerOptionText.indexOf(lowerText) != -1;
//        if (match || contains) {
//            option.selected = true;
//            return;
//        }
//        searchBoxAlleAnwendungen.selectedIndex = 0;
//    }
//});
///*******************************************************/
////Search für Selected Anwendungen


//searchBoxMeineAnwendugen.addEventListener("keyup", function (d) {
//    var text = d.target.value;
//    var options = searchedMeineAnwendungen.options;
//    for (var i = 0; i < options.length; i++) {
//        var option = options[i];
//        var optionText = option.text;
//        var lowerOptionText = optionText.toLowerCase();
//        var lowerText = text.toLowerCase();
//        var regex = new RegExp("^" + text, "i");
//        var match = optionText.match(regex);
//        var contains = lowerOptionText.indexOf(lowerText) != -1;
//        if (match || contains) {
//            option.selected = true;
//            return;
//        }
//        searchBoxMeineAnwendugen.selectedIndex = 0;
//    }
//});
///*******************************************************/
////Search für Selected Felder

//searchBoxMeineFelder.addEventListener("keyup", function (e) {
//    var text = e.target.value;
//    var options = searchedMeineFelder.options;
//    for (var i = 0; i < options.length; i++) {
//        var option = options[i];
//        var optionText = option.text;
//        var lowerOptionText = optionText.toLowerCase();
//        var lowerText = text.toLowerCase();
//        var regex = new RegExp("^" + text, "i");
//        var match = optionText.match(regex);
//        var contains = lowerOptionText.indexOf(lowerText) != -1;
//        if (match || contains) {
//            option.selected = true;
//            return;
//        }
//        searchBoxMeineFelder.selectedIndex = 0;
//    }
//});
///*******************************************************/
//// Search für alle Felder
//searchBoxAlleFelder.addEventListener("keyup", function (f) {
//    var text = f.target.value;
//    var options = searchedAlleFelder.options;
//    for (var i = 0; i < options.length; i++) {
//        var option = options[i];
//        var optionText = option.text;
//        var lowerOptionText = optionText.toLowerCase();
//        var lowerText = text.toLowerCase();
//        var regex = new RegExp("^" + text, "i");
//        var match = optionText.match(regex);
//        var contains = lowerOptionText.indexOf(lowerText) != -1;
//        if (match || contains) {
//            option.selected = true;
//            return;
//        }
//        searchBoxAlleFelder.selectedIndex = 0;
//    }
//});
///*******************************************************/