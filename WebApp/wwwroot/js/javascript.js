$(function () {
    var
        $table = $('#tree-table'),
        rows = $table.find('tr');

    rows.each(function (index, row) {
        var
            $row = $(row),
            level = $row.data('level'),
            id = $row.data('id'),
            $columnName = $row.find('td[data-column="name"]'),
            children = $table.find('tr[data-parent="' + id + '"]');

        if (children.length) {
            var expander = $columnName.prepend('' +
                '<span class="treegrid-expander fas fa-angle-right"></span>' +
                '');

            children.hide();

            expander.on('click', function (e) {
                var $target = $(e.target);
                console.log($row[0].rowIndex, children[0].rowIndex);
                if ($target.hasClass('fa-angle-right')) {
                    $target
                        .removeClass('fa-angle-right')
                        .addClass('fa-angle-down');

                    $('#tree-table > tbody > tr').eq($row[0].rowIndex).after(children);

                    children.show();
                } else {
                    /*$target
                        .removeClass('fa-angle-down')
                        .addClass('fa-angle-right');*/

                    reverseHide($table, $row);
                }
            });
        }

        $columnName.prepend('' +
            '<span class="treegrid-indent" style="width:' + 20 * level + 'px"></span>' +
            '');
    });

    // Reverse hide all elements
    reverseHide = function (table, element) {
        var
            $element = $(element),
            id = $element.data('id'),
            children = table.find('tr[data-parent="' + id + '"]');

        if (children.length) {
            children.each(function (i, e) {
                reverseHide(table, e);
            });

            $element
                .find('.fa-angle-down')
                .removeClass('fa-angle-down')
                .addClass('fa-angle-right');

            children.hide();
        }
    };
});
