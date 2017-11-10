function selectWhiteCard(card) {
    $(".white-card.active").removeClass("active");
    card.addClass("active");

    $(".card-number").text(card.text());
    $(".blank").text(card.data("answer"));
    $(".winner-name").text(card.data("owner"));
}

function createSubmittedPlayerItem(name) {
    var newItem = $("<li/>", {
        text: name
    });
    return newItem;
}

function createWhiteCardItem(index, answer, owner) {
    var newItem = $("<a />", {
        href: "#",
        class: "white-card list-group-item",
        text: index,
        click: function () {
            selectWhiteCard($(this));
            return false;
        }
    });
    newItem.data("answer", answer);
    newItem.data("owner", owner);
    return newItem;
}

$(document).ready(function () {
    $(".refresh").click(function () {
        $.ajax({
            url: "/api/SubmittedCards/Players/",
            method: "GET",
            success: function (data) {
                $("#submittedPlayers").html("");
                $.each(data, function () {
                    $("#submittedPlayers").append(createSubmittedPlayerItem(this));
                });
            }
        });
        return false;
    });

    $(".white-card").click(function () {
        selectWhiteCard($(this));
        return false;
    });

    $(".go-button").click(function () {
        $.ajax({
            url: "/api/SubmittedCards/",
            method: "GET",
            success: function (data) {
                var i = 1;
                $("#white-card-group").html("");
                $.each(data, function () {
                    $("#white-card-group").append(createWhiteCardItem(i, this.Answer, this.Owner));
                    i++;
                });
                selectWhiteCard($(".white-card").first());
                $(".answers").show();
            }
        });
        
        return false;
    });

    $(".select-answer").click(function () {
        $(".submitted").hide();
        $(".answers").hide();
        $(".winner-display").show();
        return false;
    });

    $(".go-back").click(function () {
        $(".submitted").show();
        $(".answers").show();
        $(".winner-display").hide();
        return false;
    });

    $(".submit-answer").click(function () {
        $("input, textarea, .btn").attr("disabled", true);

        var name = $("#input-player-name").val();
        var answer = $("#input-player-answer").val();

        $.ajax({
            url: "/api/submittedcards/" + name,
            method: "PUT",
            data: {"": answer},
            success: function () {
                $(".submit-successful-message").show();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $(".submit-failed-message-text").text(errorThrown);
                $(".submit-failed-message").show();
            },
            complete: function () {
                $("input, textarea, .btn").removeAttr("disabled");
            }
        });

        return false;
    });

    $(".get-whitecard").click(function () {
        $.ajax({
            url: "/api/whitecards/next",
            method: "GET",
            success: function (data) {
                $("#input-player-answer").text(data.Text);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $(".submit-failed-message-text").text(errorThrown);
                $(".submit-failed-message").show();
            }
        });

        return false;
    });

    $(".close-button").click(function () {
        $(this).parent(".closable").hide();
        return false;
    });
});