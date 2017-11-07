MyUtil = new Object();
MyUtil.selectFilterData = new Object();

MyUtil.selectFilter = function (selectId, filter) {
    var list = document.getElementById(selectId);
    if (!MyUtil.selectFilterData[selectId]) { //if we don't have a list of all the options, cache them now'
        MyUtil.selectFilterData[selectId] = new Array();
        for (var i = 0; i < list.options.length; i++) {
            MyUtil.selectFilterData[selectId][i] = list.options[i];
        }
    }
    list.options.length = 0;   //remove all elements from the list
    for (var i = 0; i < MyUtil.selectFilterData[selectId].length; i++) { //add elements from cache if they match filter
        var o = MyUtil.selectFilterData[selectId][i];
        if (o.text.toLowerCase().indexOf(filter.toLowerCase()) >= 0) {
            list.add(o, null);
        }
    }
}

MyUtil.selectFilter = function (selectId, filter) {
    $("#" + selectId).find('option').hide();
    $("#" + selectId).find('option:Contains("' + filter + '")').show();
}

$('input:checkbox').on('change', function () {
    //if(this.checked)    // optional, depends on what you want
    $('input[value="' + this.value + '"]:checkbox').prop('checked', this.checked);
});


$("select").mousedown(function (e) {
    e.preventDefault();

    var select = this;
    var scroll = select.scrollTop;

    e.target.selected = !e.target.selected;

    setTimeout(function () { select.scrollTop = scroll; }, 0);

    $(select).focus();
}).mousemove(function (e) { e.preventDefault() });

$('#btnRightDatentyp').click(function (e) {
   
    var selectedDatentypen = $('#selectedDatentypen option:selected');
    if (selectedDatentypen.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#alleDatentypen').append($(selectedDatentypen).clone());
    $(selectedDatentypen).remove();
    e.preventDefault();
});

$('#btnLeftDatentyp').click(function (e) {
    var selectedDatentypen = $('#alleDatentypen option:selected');
    if (selectedDatentypen.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#selectedDatentypen').append($(selectedDatentypen).clone());
    $(selectedDatentypen).remove();
    e.preventDefault();
});


$('#btnSubmitDatentyp').click(function (e) {
    $('#selectedDatentypen option').prop('selected', true);
});

/*******************************************************/

$('#btnRightAnwendung').click(function (e) {
    var selectedAnwendungen = $('#selectedAnwendungen option:selected');
    if (selectedAnwendungen.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#alleAnwendungen').append($(selectedAnwendungen).clone());
    $(selectedAnwendungen).remove();
    e.preventDefault();
});

$('#btnLeftAnwendung').click(function (e) {
    var selectedAnwendungen = $('#alleAnwendungen option:selected');
    if (selectedAnwendungen.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#selectedAnwendungen').append($(selectedAnwendungen).clone());
    $(selectedAnwendungen).remove();
    e.preventDefault();
});

$('#btnSubmit').click(function (e) {
    $('#selectedAnwendungen option').prop('selected', true);
    $('#selectedFelder option').prop('selected', true);
});


/*******************************************************/

$('#btnRightFeld').click(function (e) {
    var selectedFelder = $('#selectedFelder option:selected');
    if (selectedFelder.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#alleFelder').append($(selectedFelder).clone());
    $(selectedFelder).remove();
    e.preventDefault();
});

$('#btnLeftFeld').click(function (e) {
    var selectedFelder = $('#alleFelder option:selected');
    if (selectedFelder.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#selectedFelder').append($(selectedFelder).clone());
    $(selectedFelder).remove();
    e.preventDefault();
});