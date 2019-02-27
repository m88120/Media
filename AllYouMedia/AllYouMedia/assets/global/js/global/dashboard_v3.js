
!function (document, window, $) {
    "use strict";
    /*---- Bar chart ----*/
    $(".stackline-bar").sparkline("html", {
        type: "bar",
        height: "50px",
        barWidth: 10,
        barSpacing: 5,
        barColor: ['#087380'],
        negBarColor: ['#c9302c'],
        stackedBarColor: ['#087380', "#c9302c"]
    });

    /*---- Line chart ----*/
    $(".sparkline-line").sparkline("html", {
        height: "50px",
        width: "100px",
        lineColor: ['#449d44'],
        fillColor: ['transparent']
    });

    /*---- inline range chart ----*/
    $(".sparkline-inlinerange").sparkline("html", {
        fillColor: !1,
        height: "50px",
        width: "100px",
        lineColor: ['#c9302c'],
        spotColor: ['#087380'],
        minSpotColor: ['#087380'],
        maxSpotColor: ['#087380'],
        normalRangeColor: ['transparent'],
        normalRangeMin: -1,
        normalRangeMax: 8
    });

    /*---- line custom chart ----*/
    $(".sparkline-linecustom").sparkline("html", {
        height: "50px",
        width: "100px",
        lineColor: ['#f0ad4e'],
        fillColor: ['transparent'],
        minSpotColor: !1,
        maxSpotColor: !1,
        spotColor: ['green'],
        spotRadius: 2
    });
    var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    /*---- Area chart ----*/
    Morris.Area({
        //element: 'area-chart',
        //data: [
        //    { m: 'Jan', a: 100, b: 100 },
        //    { m: 'Feb', a: 100, b: 25 },
        //    { m: 'Mar', a: 40, b: 40 },
        //    { m: 'Apr', a: 50, b: 10 },
        //    { m: 'May', a: 35, b: 65 },
        //    { m: 'Jun', a: 65, b: 15 },
        //    { m: 'Jul', a: 100, b: 20 },
        //    { m: 'Aug', a: 60, b: 100 },
        //    { m: 'Sept', a: 70, b: 140 },
        //    { m: 'Oct', a: 80, b: 110 },
        //    { m: 'Nov', a: 90, b: 120 },
        //    { m: 'Dec', a: 70, b: 130 },
        //], 
        //xkey: 'm',
        //ykeys: ['a', 'b'],
        //lineWidth: '0',
        //lineColors: ['#087380', '#c9302c'],
        //pointSize: 0,
        //hoverpointSize: 0,
        //fillOpacity: 0.25,
        //labels: ['SALES', 'FAN'],
        //resize: true
        element: 'area-chart',
        data: [
             {
            m: '2017-03',
            a: 24,
            b: 0
        }, {
            m: '2017-04',
            a: 29,
            b: 0
        }, {
            m: '2017-05',
            a: 38,
            b: 0
        }, {
            m: '2017-06',
            a: 46,
            b: 0
        }, {
            m: '2017-07',
            a: 52,
            b: 0
        }, {
            m: '2017-08',
            a: 48,
            b:0
        }, {
            m: '2017-09',
            a: 42,
            b: 0
        }, {
            m: '2017-10',
            a: 50,
            b: 0
        }, {
            m: '2017-11',
            a: 58,
            b: 0
        }, {
            m: '2017-12',
            a: 66,
            b: 0
        },
        {
            m: '2018-01',
            a: 79,
            b: 0
        },
        {
            m: '2018-02',
            a: 70,
            b: 0
        }, {
            m: '2018-03',
            a: 80,
            b: 0
        },
        ],
        xkey: 'm',
        ykeys: ['a', 'b'],
        lineWidth: 2,
        lineColors: ['#087380', '#c9302c'],
        pointSize: 3,
        hoverpointSize: 0,
        fillOpacity: 0.5,
        labels: ['SALES ($)', 'FAN'],
        xLabelFormat: function (x) {
            var IndexToMonth = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
            var month = IndexToMonth[x.getMonth()];
            var year = x.getFullYear();
            return month + ' ' + year;
        },
        dateFormat: function (x) {
            var IndexToMonth = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
            var month = IndexToMonth[new Date(x).getMonth()];
            var year = new Date(x).getFullYear();
            return  month + ' ' + year;
        },
        resize: true
    });

    /*---- Pie chart ----*/
    $(".sparkline-pie").sparkline("html", {
        type: "pie",
        height: "350px",
        sliceColors: ['#36a9e1', '#4fbcf1', '#71ccf9']
    });

    /*---- Bars charts ----*/
    // Morris.Bar({
    //     element: 'bar-charts',
    //     data: [
    //         {x: '2016 Q1', y: 4, z: 1, a: 2},
    //         {x: '2016 Q2', y: 3, z: 4, a: null},
    //         {x: '2016 Q3', y: 2, z: 3, a: 4},
    //         {x: '2016 Q4', y: 1, z: 3, a: 2}
    //     ],
    //     xkey: 'x',
    //     ykeys: ['y', 'z', 'a'],
    //     labels: ['Y', 'Z', 'A'],
    //     barColors: ['#087380', '#c9302c', '#ec971f'],
    //     resize: true
    // });

    /*---- Owl slider ----*/
    $("#owl-full").owlCarousel({
        navigation: true,
        slideSpeed: 400,
        paginationSpeed: 500,
        items: 1
    });

    $("#owl-full-testimonials").owlCarousel({
        navigation: true,
        slideSpeed: 400,
        paginationSpeed: 500,
        items: 1
    });

    /*---- Vector Map ----*/
    $('#world-map').vectorMap({
        map: 'world_en',
        color: '#ffffff',
        hoverOpacity: 0.7,
        backgroundColor: '#eaeaea',
        selectedColor: '#087380',
        borderOpacity: 0.25,
        borderColor: '#fff',
        enableZoom: true,
        showTooltip: true,
        values: sample_data,
        scaleColors: ['#C8EEFF', '#087380'],
        normalizeFunction: 'polynomial'
    });

    /*---- Line CHART ----*/
    var chart = new Chartist.Line('.smil-animation', {
        labels: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
        series: [
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
        ]
    }, {
        low: 0
    });

    // Let's put a sequence number aside so we can use it in the event callbacks
    var seq = 0,
        delays = 80,
        durations = 500;

    // Once the chart is fully created we reset the sequence
    chart.on('created', function () {
        seq = 0;
    });

    // On each drawn element by Chartist we use the Chartist.Svg API to trigger SMIL animations
    chart.on('draw', function (data) {
        seq++;

        if (data.type === 'line') {
            // If the drawn element is a line we do a simple opacity fade in. This could also be achieved using CSS3 animations.
            data.element.animate({
                opacity: {
                    // The delay when we like to start the animation
                    begin: seq * delays + 1000,
                    // Duration of the animation
                    dur: durations,
                    // The value where the animation should start
                    from: 0,
                    // The value where it should end
                    to: 1
                }
            });
        } else if (data.type === 'label' && data.axis === 'x') {
            data.element.animate({
                y: {
                    begin: seq * delays,
                    dur: durations,
                    from: data.y + 100,
                    to: data.y,
                    // We can specify an easing function from Chartist.Svg.Easing
                    easing: 'easeOutQuart'
                }
            });
        } else if (data.type === 'label' && data.axis === 'y') {
            data.element.animate({
                x: {
                    begin: seq * delays,
                    dur: durations,
                    from: data.x - 100,
                    to: data.x,
                    easing: 'easeOutQuart'
                }
            });
        } else if (data.type === 'point') {
            data.element.animate({
                x1: {
                    begin: seq * delays,
                    dur: durations,
                    from: data.x - 10,
                    to: data.x,
                    easing: 'easeOutQuart'
                },
                x2: {
                    begin: seq * delays,
                    dur: durations,
                    from: data.x - 10,
                    to: data.x,
                    easing: 'easeOutQuart'
                },
                opacity: {
                    begin: seq * delays,
                    dur: durations,
                    from: 0,
                    to: 1,
                    easing: 'easeOutQuart'
                }
            });
        } else if (data.type === 'grid') {
            // Using data.axis we get x or y which we can use to construct our animation definition objects
            var pos1Animation = {
                begin: seq * delays,
                dur: durations,
                from: data[data.axis.units.pos + '1'] - 30,
                to: data[data.axis.units.pos + '1'],
                easing: 'easeOutQuart'
            };

            var pos2Animation = {
                begin: seq * delays,
                dur: durations,
                from: data[data.axis.units.pos + '2'] - 100,
                to: data[data.axis.units.pos + '2'],
                easing: 'easeOutQuart'
            };

            var animations = {};
            animations[data.axis.units.pos + '1'] = pos1Animation;
            animations[data.axis.units.pos + '2'] = pos2Animation;
            animations['opacity'] = {
                begin: seq * delays,
                dur: durations,
                from: 0,
                to: 1,
                easing: 'easeOutQuart'
            };

            data.element.animate(animations);
        }
    });

    // For the sake of the example we update the chart every time it's created with a delay of 10 seconds
    chart.on('created', function () {
        if (window.__exampleAnimateTimeout) {
            clearTimeout(window.__exampleAnimateTimeout);
            window.__exampleAnimateTimeout = null;
        }
        window.__exampleAnimateTimeout = setTimeout(chart.update.bind(chart), 12000);
    });





    // Create a simple bi-polar bar chart
    var chart = new Chartist.Bar('.peak-circle', {
        labels: ['W1', 'W2', 'W3', 'W4', 'W5', 'W6', 'W7', 'W8', 'W9', 'W10'],
        series: [
            [2, 4, 8, 3, 1, -2, -1, -3, -6, -2]
        ]
    }, {
        high: 10,
        low: -10,
        axisX: {
            labelInterpolationFnc: function (value, index) {
                return index % 2 === 0 ? value : null;
            }
        }
    });

    // Listen for draw events on the bar chart
    chart.on('draw', function (data) {
        // If this draw event is of type bar we can use the data to create additional content
        if (data.type === 'bar') {
            // We use the group element of the current series to append a simple circle with the bar peek coordinates and a circle radius that is depending on the value
            data.group.append(new Chartist.Svg('circle', {
                cx: data.x2,
                cy: data.y2,
                r: Math.abs(Chartist.getMultiValue(data.value)) * 2 + 5
            }, 'ct-slice-pie'));
        }
    });






}(document, window, jQuery);
