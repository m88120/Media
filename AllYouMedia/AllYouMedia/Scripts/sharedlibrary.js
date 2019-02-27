function cSharpDateToJavascriptDate(dateString) {
    if (dateString.indexOf('Date') >= 0) { //Format: /Date(1320259705710)/
        return new Date(
            parseInt(dateString.substr(6), 10)
        );
    } else if (dateString.length == 10) { //Format: 2011-01-01
        return new Date(
            parseInt(dateString.substr(0, 4), 10),
            parseInt(dateString.substr(5, 2), 10) - 1,
            parseInt(dateString.substr(8, 2), 10)
        );
    } else if (dateString.length == 19) { //Format: 2011-01-01 20:32:42
        return new Date(
            parseInt(dateString.substr(0, 4), 10),
            parseInt(dateString.substr(5, 2), 10) - 1,
            parseInt(dateString.substr(8, 2, 10)),
            parseInt(dateString.substr(11, 2), 10),
            parseInt(dateString.substr(14, 2), 10),
            parseInt(dateString.substr(17, 2), 10)
        );
    } else {
        this._logWarn('Given date is not properly formatted: ' + dateString);
        return 'format error!';
    }
}
function cSharpDateToFormatedDate(dateString) {
    return $.datepicker.formatDate('dd-mm-yy', cSharpDateToJavascriptDate(dateString));
}