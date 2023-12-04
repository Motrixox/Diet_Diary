$('#searchBox').keyup(function () {
    var searchText = $(this).val();
    if (searchText.length >= 3) {
        $.ajax({
            type: "POST",
            url: "/Meal/GetSuggestions", // �cie�ka do akcji GetSuggestions w kontrolerze Search
            data: { searchText: searchText },
            success: function (response) {
                var suggestionsPanel = $('#suggestionsPanel');
                suggestionsPanel.empty();

                if (response.length > 0) {
                    suggestionsPanel.html(response);
                }
                else {
                    suggestionsPanel.html('<div class="no-suggestions">Brak podpowiedzi</div>');
                }

                suggestionsPanel.show();
            },
            error: function (response) {
                // Obs�uga b��d�w
            }
        });
    }
    else {
        $('#suggestionsPanel').hide();
    }
});
