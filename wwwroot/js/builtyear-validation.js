// Built Year Validation
jQuery.validator.addMethod("builtyear", function (value, element, param) {

    if (value === "") return false;

    var year = parseInt(value);
    if (isNaN(year)) return false;

    var maxYears = parseInt(param);
    var currentYear = new Date().getFullYear();

    // must be a past year
    if (year >= currentYear) return false;

    // not older than maxYears (150 years)
    var oldestAllowed = currentYear - maxYears;
    return year >= oldestAllowed;

}, "Invalid built year.");

// Map .NET attribute → JS validator
jQuery.validator.unobtrusive.adapters.addSingleVal("builtyear", "maxyears");
