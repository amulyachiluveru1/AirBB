jQuery.validator.addMethod("bathrooms", function (value, element) {
    if (value === "") return false;
    var num = parseFloat(value);
    if (isNaN(num)) return false;
    var frac = num - Math.floor(num);
    // allow integer or .5
    if (frac === 0 || Math.abs(frac - 0.5) < 0.00001) return true;
    return false;
});
jQuery.validator.unobtrusive.adapters.add("bathrooms", [], function (options) {
    options.rules["bathrooms"] = true;
    if (options.message) options.messages["bathrooms"] = options.message;
});
