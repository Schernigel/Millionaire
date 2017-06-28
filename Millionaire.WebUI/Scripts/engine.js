var lose = false;
var level = 1;
var diagramtrue = false;


$(document).ready(function () {
    
    
    eventButtonOn();
    eventHelpOn();
    

    $("#next_button").click(function () {

        if (lose === true) {
            var url = "ShowResult";
            $(location).attr('href', url);
        }
        
        
        if (level == 16) {
            takeMoney();
            
        } else {
            getQuestion(level);
            backStyles();
            $("#next_button").fadeOut(500);
            $("#logo_image").fadeIn(500);
            $("#p" + (level-1)).removeClass("active");
            $("#p" + level).addClass("active");
        }

        eventButtonOn();
        level++;
    });

    $("#next_button").hide();
    $("#diagramAuditoryid").hide();
    $("#fifty_end").hide();
    $("#friend_end").hide();
    $("#auditory_end").hide();
    getQuestion(level);
});


function eventButtonOn() {
    $("#answer_a").on("click", handlerButton);
    $("#answer_b").on("click", handlerButton);
    $("#answer_c").on("click", handlerButton);
    $("#answer_d").on("click", handlerButton);
    $("#answer_a").show();
    $("#answer_b").show();
    $("#answer_c").show();
    $("#answer_d").show();

    
}
function eventButtonOff() {
    $("#answer_a").off("click", handlerButton);
    $("#answer_b").off("click", handlerButton);
    $("#answer_c").off("click", handlerButton);
    $("#answer_d").off("click", handlerButton);
}

function eventHelpOn() {
    $("#fifty").on("click", handlerButtonFifty);
    $("#friend").on("click", handlerButtonFriend);
    $("#auditory").on("click", handlerButtondiagram);
}

function handlerButtonFifty(parameters) {
    $("#fifty1").removeClass("ch_img_1");

    $("#fifty").addClass("used_fifty");
    $("#fifty_end").show();

    var id = $("#questionId").attr("value");
    $.getJSON("/Game/GetFiftyFifty/", { questionId: id }, function(data) { removeIncorectAnswer( data) });

}

function arrDiff(a1, a2) {

    var a = [], diff = [];

    for (var i = 0; i < a1.length; i++) {
        a[a1[i]] = true;
    }

    for (var i = 0; i < a2.length; i++) {
        if (a[a2[i]]) {
            delete a[a2[i]];
        } else {
            a[a2[i]] = true;
        }
    }

    for (var k in a) {
        diff.push(k);
    }

    return diff;
};

function removeIncorectAnswer(parameters) {
    var ansarray = [1, 2, 3, 4];
    var corectarray = parameters;

    var incorect = arrDiff(ansarray, corectarray);

    
    for (var i = 0; i < incorect.length; i++) {
        switch (incorect[i]) {
            case "1":
                removeAnswer("a");
                break;
            case "2":
                removeAnswer("b");
                break;
            case "3":
                removeAnswer("c");
                break;
            case "4":
                removeAnswer("d");
                break;
        }
    }
    
}

function removeAnswer(value) {
    $("#answer_" + value).hide();
    
}



function handlerButtonFriend(event) {
    var question = ($("#question").text()).replace(/\s/g, "+");
    var url = "https://www.google.com.ua/search?q="+question;
    window.open(url);
    $("#friend1").removeClass("ch_img_2");
    
    $("#friend").addClass("used_friend"); 
    $("#friend_end").show();
    
}

function handlerButtondiagram(parameters) {
    $("#diagramAuditoryid").show();
    $("#logo_image").hide();
    diagramtrue = true;
    diagram();
    $("#auditory1").removeClass("ch_img_3");

    $("#auditory").addClass("used_auditory");
    $("#auditory_end").show();
}





function handlerButton(event) {
    var id = $(event.currentTarget).attr("id");
    var answerNumber = 0;
    if (diagramtrue === true) {
        $("#diagramAuditoryid").hide();
    }
    
    switch (id) {
        case "answer_a":
            answerNumber = 1;
            break;
        case "answer_b":
            answerNumber = 2;
            break;
        case "answer_c":
            answerNumber = 3;
            break;
        case "answer_d":
            answerNumber = 4;
            break;
    }
    
    var ansid = $("#questionId").attr("value");
    isAnswerCorrect(this, answerNumber, ansid);
    saveStatistic(answerNumber);
    eventButtonOff();
};

function saveStatistic(answerNumber) {
    var qustionId = $("#questionId").attr("value");
    $.ajax({
        type: "POST",
        url: "/Game/CreateStatisticRecord",
        data: { answerNumber, qustionId},
        success: function (data){}
        
    });
}




function backStyles() {
    $("#answer_a").removeClass();
    $("#answer_b").removeClass();
    $("#answer_c").removeClass();
    $("#answer_d").removeClass();

    $("#answer_a").addClass("ans_1 answer_efect");
    $("#answer_b").addClass("ans_2 answer_efect");
    $("#answer_c").addClass("ans_3 answer_efect");
    $("#answer_d").addClass("ans_4 answer_efect");
}

function isAnswerCorrect(obj,number,id) {
    $.getJSON("/Game/IsAnswerCorrect/", { answerNumber: number, id: id }, function (data) { nextQustionOrEnd(obj, data) });
}

function nextQustionOrEnd(obj,data) {
    var val = JSON.parse(data);
    if (val) {
        RightImage(obj);
        if (level == 15) {
            ShowNextOrEndButton("Вы победили!!!!!!");
        } else {
            ShowNextOrEndButton("Следующий вопрос");
            var money = $("#p" + level).text();
            $("#take_money").attr("value", money);
        }

    } else {
        WrongImage(obj);
        ShowNextOrEndButton("Вы проиграли!");
        lose = true;

    }
    
}

function RightImage(item) {
    var $el = item;
    $el.classList.add("right-answer", "no-hover");
    
}
function WrongImage(item) {
    var $el = item;
    $el.classList.add("wrong-answer", "no-hover");
   
}

function ShowNextOrEndButton(message) {
    var $el = $("#next_button");
    $el.fadeIn(1000);
    $("#next").text(message);
    $("#logo_image").fadeOut(1000);
    //$el.removeClass("visible_next_false");
    //$el.addClass("visible_next_true");
    //$(".logo").removeClass("visible_logo_true");
    //$(".logo").addClass("visible_logo_false");

}

function getQuestion(lev) {
    $.getJSON("/Game/GetQuestion", { level: lev },
        function (data) {
            $("#question").empty();
            $("#a").empty();
            $("#b").empty();
            $("#c").empty();
            $("#d").empty();
            $("#answerId").attr("value", "");

            
            $("#question").append(data.GameQuestion);
            $("#a").append("<span>A: </span>"+data.First);
            $("#b").append("<span>B: </span>"+data.Second);
            $("#c").append("<span>C: </span>"+data.Third);
            $("#d").append("<span>D: </span>"+data.Fourth);
            $("#questionId").attr("value",data.Id);
        });
}



function diagram(parameters) {
    var dataset = [
        {
            value: 5,
            color: '#dc3912'
        }, {
            value: 40,
            color: '#ff9900'
        }, {
            value: 30,
            color: '#109618'
        }, {
            value: 25,
            color: '#990099'
        }
    ];

    var maxValue = 25;
    var container = $('.diagramAuditory');

    var addSector = function (data, startAngle, collapse) {
        var sectorDeg = 3.6 * data.value;
        var skewDeg = 90 + sectorDeg;
        var rotateDeg = startAngle;
        if (collapse) {
            skewDeg++;
        }

        var sector = $('<div>', {
            'class': 'sector'
        }).css({
            'background': data.color,
            'transform': 'rotate(' + rotateDeg + 'deg) skewY(' + skewDeg + 'deg)'
        });
        container.append(sector);

        return startAngle + sectorDeg;
    };

    dataset.reduce(function (prev, curr) {
        return (function addPart(data, angle) {
            if (data.value <= maxValue) {
                return addSector(data, angle, false);
            }

            return addPart({
                value: data.value - maxValue,
                color: data.color
            }, addSector({
                value: maxValue,
                color: data.color,
            }, angle, true));
        })(curr, prev);
    }, 0);
}

function takeMoney() {

    if (level == 16) {
        level--;
    }
    var money = parseInt($("#p"+level).text());
    $.ajax({
        type: "POST",
        url: "/Game/TakeMoney",
        data: { money, level },
        success: function(data) {
            var url = "ShowResult";
            $(location).attr('href', url);
        }

    });
}


