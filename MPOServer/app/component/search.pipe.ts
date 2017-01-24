import { Pipe, PipeTransform } from 'angular2/core';

@Pipe({
    name: 'filter',
    pure: false
})
export class SearchPipe implements PipeTransform {
    transform(value, searchinput) {
        if (!searchinput[0]) {
            return value;
        } else if (value) {
            return value.filter(item => {
                for (let key in item) {
                    if ((typeof item[key] === 'string' || item[key] instanceof String) &&
                        (item[key].indexOf(searchinput[0]) !== -1)) {
                        return true;
                    }
                }
            });
        }
    }

    //transform(items, searchinput) {
    //    if (!searchinput[0]) {
    //        return items;
    //    }
    //    let ans = [];
    //    for (let k in items) {
    //        if (items[k].MachineNameAlias.match('^.*' + searchinput + '.*$')
    //            || items[k].MyRegionCode.match('^.*' + searchinput + '.*$')
    //            || items[k].IPAddress.match('^.*' + searchinput + '.*$')
    //            || items[k].StoreID.match('^.*' + searchinput + '.*$')) {
    //            ans.push({ key: k, value: items[k] });
    //        }
    //    }
    //    return ans;
    //}
}