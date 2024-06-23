$(function () {
   
    $('#submitBtn').click(function (e) {
        e.preventDefault();
        var selectedArticles = [];
        $('input[name="art_selected[]"]:checked').each(function () {
            selectedArticles.push($(this).val());
        });

        var formData = {
            id: selectedArticles.join(','), // Assuming you are sending multiple IDs for batch update
            niv: $('input[name="niveau"]').val(),
            rayon: $('input[name="rayon"]').val(),
            range: $('input[name="rangee"]').val(),
            idEntrepot: $('input[name="ref_entrepot"]').val(),
            desc: 'Updated description'
        };

        var miseEnPlaceUrl = $('#submitBtn').data('mise-en-place-url');
        var indexUrl = $('#submitBtn').data('index-url');

        $.ajax({
            type: "POST",
            url: miseEnPlaceUrl,
            data: JSON.stringify(formData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.isSuccess) {
                    alert(response.message);
                    location.replace(indexUrl);
                } else {
                    alert(response.message);
                }
            },
            error: function (err) {
                alert("Une erreur est survenue.");
            }
        });
    });
});