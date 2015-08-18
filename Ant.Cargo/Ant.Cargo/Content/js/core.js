var Core = {};

Core.FindInArray = function (array, keyName, keyValue) {
    if (!array) {
        return null;
    }

    var result = [];

    $.grep(array, function (n) {
        if (n[keyName] == keyValue) {
            result.push(n);
        }
    })

    if (result.length > 0) {
        return result[0];
    }

    return null;
}

Core.RemoveFromArray = function (array, keyName, keyValue) {
    var i = array.length;
    while (i--) {
        if (array[i]
            && array[i].hasOwnProperty(keyName)
            && (arguments.length > 2 && array[i][keyName] === keyValue)) {

            array.splice(i, 1);

        }
    }
    return array;
}

Core.JoinArrayProperties = function (array, KeyName, delimiter) {
    var result = '';

    array.forEach(function (item) {
        result += item[KeyName];
        if (item != array[array.length - 1]) {
            result += delimiter;
        }
    });

    return result;
}

Core.SelectArrayProperties = function (array, KeyName) {
    var result = [];

    angular.forEach(array, function (item) {
        result.push(item[KeyName]);
    });

    return result;
}