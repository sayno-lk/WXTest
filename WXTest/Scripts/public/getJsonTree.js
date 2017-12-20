//生成一个模拟树
var getJsonTree = function (data, parentId) {
    var itemArr = [];
    for (var i = 0; i < data.length; i++) {
        var node = data[i];
        //console.log(node);
        //data.splice(i, 1)
        if (node.parent_id == parentId) {
            var newNode = { id: node.product_id, name: node.product_name, children: getJsonTree(data, node.product_id) };
            itemArr.push(newNode);
        }
    }
    return itemArr;
}

var createTree = function (node, parentId) {
    parentId = parentId || 0;
    var childArr = [];
    layui.each(node, function (index, item) {
        //console.log(item);
        if (item.parent_id == parentId) {
            var child =
              {
                  id: item.product_id,
                  name: item.product_name,
                  make: item.product_mark,
                  level: item.product_level,
                  children: getJsonTree(node, item.product_id)
              };
            childArr.push(child);
        }
    });
    return childArr;
};