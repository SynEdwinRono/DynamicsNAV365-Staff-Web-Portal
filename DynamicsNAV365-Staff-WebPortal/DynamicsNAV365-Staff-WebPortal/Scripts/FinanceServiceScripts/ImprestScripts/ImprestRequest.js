//Initialize imprest request
function InitializeImprestRequest() {
	var dateToday = '01/01/2020';
	$("#DateFrom").datepicker({
		dateFormat: "dd/mm/yy",
		changeMonth: true,
		changeYear: true,
		minDate: dateToday
	});

	$("#DateTo").datepicker({
		dateFormat: "dd/mm/yy",
		changeMonth: true,
		changeYear: true,
		minDate: dateToday
	});

	//Add dropdown search
	AddImprestRequestDropDownListSearch();

	//On change events
	AddOnChangeEvents();
}

function AddOnChangeEvents() {

	$("#ImprestCode").change(function () {

		if ($(this).val() == "TRAVELLING LOCAL") {

			$("#LineAmountError").text("");

			$("#FromCity").css("background-color", "White");
			$("#ToCity").css("background-color", "White");
			$("#Days").css("background-color", "White");
			$("#LineAmount").css("background-color", "LightGray");

			$("#LineAmount").attr("disabled", "disabled");
			$("#FromCity").removeAttr("disabled");
			$("#ToCity").removeAttr("disabled");
			$("#Days").removeAttr("disabled");
		}

		else if ($(this).val() == "TRAVELLING FOREIGN") {
			$("#LineAmountError").text("");

			$("#FromCity").css("background-color", "White");
			$("#ToCity").css("background-color", "White");
			$("#Days").css("background-color", "White");
			$("#LineAmount").css("background-color", "LightGray");

			$("#LineAmount").attr("disabled", "disabled");
			$("#FromCity").removeAttr("disabled");
			$("#ToCity").removeAttr("disabled");
			$("#Days").removeAttr("disabled");

			$("#FromCity").change(function () {
				GetAllowanceMatrix($(this).val());
			});
		}

		else {

			$("#FromCityError").text("");
			$("#ToCityError").text("");
			$("#DaysError").text("");

			$("#FromCity").css("background-color", "LightGray");
			$("#ToCity").css("background-color", "LightGray");
			$("#Days").css("background-color", "LightGray");
			$("#LineAmount").css("background-color", "White");

			$("#LineAmount").removeAttr("disabled");
			$("#FromCity").attr("disabled", "disabled");
			$("#ToCity").attr("disabled", "disabled");
			$("#Days").attr("disabled", "disabled");
		}
	});
}

function AddImprestRequestDropDownListSearch() {
	$("#ImprestType").select2({
		placeholder: $("#ImprestTypeLbl").text(),
		allowClear: true
	});

	$("#ImprestCode").select2({
		placeholder: $("#ImprestCodeLbl").text(),
		allowClear: true
	});

	$("#FromCity").select2({
		placeholder: $("#FromCityLbl").text(),
		allowClear: true
	});

	$("#ToCity").select2({
		placeholder: $("#ToCityLbl").text(),
		allowClear: true
	});

}

//Load Foreign Cities
function GetAllowanceMatrix(FromCity) {
	var options = "";
	options += "<option>";
	options += "";
	options += "</option>";

	$.ajax({
		url: AJAXUrls.GetForeignTravelMatrix,
		type: "GET",
		dataType: "json",
		data: { FromCity: FromCity },
		cache: false,
		success: function (ForeignTravelMatrix) {
			var rows = "";
			$.each(ForeignTravelMatrix, function (i, allowanceMatrix) {
				options += "<option value='" + allowanceMatrix.Tos + "'>";
				options += allowanceMatrix.Tos;
				options += "</option>";
			});
			$("#ToCity").html(options);
		},
		error: OnError
	});
}

//Get imprest amount
function GetImprestAmount(DocumentNo) {
	var imprestAmount = 0;
	$.ajax({
		url: AJAXUrls.GetImprestAmount,
		type: "GET",
		dataType: "json",
		data: { DocumentNo: DocumentNo },
		cache: false,
		success: function (result) {
			imprestAmount = result.Amount.toLocaleString();
			$("#Amount").val(imprestAmount);
		},
		error: OnError
	});
}

//Load imprest lines
function LoadImprestLines(DocumentNo) {
	$.ajax({
		url: AJAXUrls.GetImprestLinesAjax,
		type: "GET",
		dataType: "json",
		data: { DocumentNo: DocumentNo },
		cache: false,
		success: function (imprestLines) {
			var rows = "";
			$.each(imprestLines, function (i, imprestLine) {
				rows += "<tr>";
				rows += "<td>" + imprestLine.ImprestCode + "</td>";
				rows += "<td>" + imprestLine.LineDescription + "</td>";
				rows += "<td>" + imprestLine.LineAmount.toLocaleString() + "</td>";
				rows += "<td>" + imprestLine.FromCity + "</td>";
				rows += "<td>" + imprestLine.ToCity + "</td>";
				rows += '<td><a href="#" onclick="return EditImprestLine(' + imprestLine.LineNo + ',\'' + imprestLine.DocumentNo + '\');">Edit</a> | <a href="#" onclick="DeleteImprestLine(' + imprestLine.LineNo + ',\'' + imprestLine.DocumentNo + '\')">Delete</a></td>';
				rows += "</tr>";
				$("#ImprestLineTbl tbody").html(rows);
			});
			$("#AjaxLoader").css("display", "none");
			$("#ImprestLineTbl").css("display", "block");
		},
		error: function (xhr, status, thrownError) {
			rows += "<tr>";
			rows += "<td class='text-danger text-center' colspan='8'>Unable to load the imprest lines.</td>";
			rows += "</tr>";
			$("#ImprestLineTbl tbody").html(rows);
			$("#AjaxLoader").css("display", "none");
			$("#ImprestLineTbl").css("display", "block");
		}
	});
}

function ViewImprestLines(DocumentNo) {
	$.ajax({
		url: AJAXUrls.GetImprestLinesAjax,
		type: "GET",
		dataType: "json",
		data: { DocumentNo: DocumentNo },
		cache: false,
		success: function (imprestLines) {
			var rows = "";
			$.each(imprestLines, function (i, imprestLine) {
				rows += "<tr>";
				rows += "<td>" + imprestLine.ImprestCode + "</td>";
				rows += "<td>" + imprestLine.LineDescription + "</td>";
				rows += "<td>" + imprestLine.LineAmount.toLocaleString() + "</td>";
				rows += "<td>" + imprestLine.FromCity + "</td>";
				rows += "<td>" + imprestLine.ToCity + "</td>";
				rows += '<td><a href="#" onclick="return ViewImprestLine(' + imprestLine.LineNo + ',\'' + imprestLine.DocumentNo + '\');">View</a> </td>';
				rows += "</tr>";
				$("#ImprestLineTbl tbody").html(rows);
			});
			$("#AjaxLoader").css("display", "none");
			$("#ImprestLineTbl").css("display", "block");
		},
		error: function (xhr, status, thrownError) {
			rows += "<tr>";
			rows += "<td class='text-danger text-center' colspan='8'>Unable to load the imprest lines.</td>";
			rows += "</tr>";
			$("#ImprestLineTbl tbody").html(rows);
			$("#AjaxLoader").css("display", "none");
			$("#ImprestLineTbl").css("display", "block");
		}
	});
}

//Create imprest line   
function CreateImprestLine() {

	var documentNo = $("#No").val();

	var validLine = ValidateImprestLine();
	if (validLine == false) {
		return false;
	}

	var ImprestLineObj = {
		DocumentNo: documentNo,
		ImprestCode: $("#ImprestCode").val(),
		LineDescription: $("#LineDescription").val(),
		LineAmount: $("#LineAmount").val(),
		Days: $("#Days").val(),
		FromCity: $("#FromCity").val(),
		ToCity: $("#ToCity").val(),
		LineGlobalDimension1Code: $("#LineGlobalDimension1Code").val(),
		LineGlobalDimension2Code: $("#LineGlobalDimension2Code").val(),
		LineShortcutDimension3Code: $("#LineShortcutDimension3Code").val(),
		LineShortcutDimension4Code: $("#LineShortcutDimension4Code").val(),
		LineShortcutDimension5Code: $("#LineShortcutDimension5Code").val(),
		LineShortcutDimension6Code: $("#LineShortcutDimension6Code").val(),
		LineShortcutDimension7Code: $("#LineShortcutDimension7Code").val(),
		LineShortcutDimension8Code: $("#LineShortcutDimension8Code").val()
	};

	$.ajax({
		url: AJAXUrls.CreateImprestLine,
		type: "POST",
		dataType: "json",
		contentType: "application/json",
		data: JSON.stringify(ImprestLineObj),
		cache: false,
		success: function (result) {
			LoadImprestLines(documentNo);
			GetImprestAmount(documentNo);
			$("#ImprestLineModal").modal("hide");
		},
		error: function (xhr, errorType, exception) {
			alert(xhr.responseText);
		}
	});
}

//Edit imprest line
function EditImprestLine(LineNo, DocumentNo) {
	$.ajax({
		url: AJAXUrls.GetImprestLineByLineNo,
		type: "GET",
		dataType: "json",
		data: { LineNo: LineNo, DocumentNo: DocumentNo },
		cache: false,
		success: function (result) {
			$('#LineNo').val(result.LineNo);
			$('#DocumentNo').val(result.DocumentNo);
			$('#ImprestCode').val(result.ImprestCode).trigger("change");
			$('#LineDescription').val(result.LineDescription).trigger("change");
			$('#LineAmount').val(result.LineAmount);
			$('#Days').val(result.Days);
			$('#FromCity').val(result.FromCity).trigger("change");
			$('#ToCity').val(result.ToCity).trigger("change");
			$('#LineGlobalDimension1Code').val(result.LineGlobalDimension1Code).trigger("change");
			$('#LineGlobalDimension2Code').val(result.LineGlobalDimension2Code).trigger("change");
			$('#LineShortcutDimension3Code').val(result.LineShortcutDimension3Code).trigger("change");
			$('#LineShortcutDimension4Code').val(result.LineShortcutDimension4Code).trigger("change");
			$('#LineShortcutDimension5Code').val(result.LineShortcutDimension5Code).trigger("change");
			$('#LineShortcutDimension6Code').val(result.LineShortcutDimension6Code).trigger("change");
			$('#LineShortcutDimension7Code').val(result.LineShortcutDimension7Code).trigger("change");
			$('#LineShortcutDimension8Code').val(result.LineShortcutDimension8Code).trigger("change");

			$("#ImprestLineModal").modal("show");
			$("#CreateImprestLineBtn").hide();
			$("#ModifyImprestLineBtn").show();
		},
		error: OnError
	});
	return false;
}

//Modify imprest line
function ModifyImprestLine() {
	var documentNo = $("#No").val();

	var validLine = ValidateImprestLine();
	if (validLine == false) {
		return false;
	}

	var imprestLineObj = {
		LineNo: $("#LineNo").val(),
		DocumentNo: documentNo,
		ImprestCode: $("#ImprestCode").val(),
		LineDescription: $("#LineDescription").val(),
		LineAmount: $("#LineAmount").val(),
		Days: $("#Days").val(),
		FromCity: $("#FromCity").val(),
		ToCity: $("#ToCity").val(),
		LineGlobalDimension1Code: $("#GlobalDimension1Code").val(),
		LineGlobalDimension2Code: $("#LineGlobalDimension2Code").val(),
		LineShortcutDimension3Code: $("#LineShortcutDimension3Code").val(),
		LineShortcutDimension4Code: $("#LineShortcutDimension4Code").val(),
		LineShortcutDimension5Code: $("#LineShortcutDimension5Code").val(),
		LineShortcutDimension6Code: $("#LineShortcutDimension6Code").val(),
		LineShortcutDimension7Code: $("#LineShortcutDimension7Code").val(),
		LineShortcutDimension8Code: $("#LineShortcutDimension8Code").val()
	};
	$.ajax({
		url: AJAXUrls.ModifyImprestLine,
		type: "POST",
		dataType: "json",
		contentType: "application/json",
		data: JSON.stringify(imprestLineObj),
		cache: false,
		success: function (result) {
			LoadImprestLines(documentNo);
			GetImprestAmount(documentNo);
			$("#ImprestLineModal").modal("hide");

			ClearImprestLineModal();
		},
		error: OnError
	});
}

//View imprest line
function ViewImprestLine(LineNo, DocumentNo) {
	$.ajax({
		url: AJAXUrls.GetImprestLineByLineNo,
		type: "GET",
		dataType: "json",
		data: { LineNo: LineNo, DocumentNo: DocumentNo },
		cache: false,
		success: function (result) {
			$("#LineNo").val(result.LineNo);
			$("#DocumentNo").val(result.DocumentNo);
			$("#ImprestCode").val(result.ImprestCode).trigger("change");
			$("#LineDescription").val(result.LineDescription).trigger("change");
			$("#LineAmount").val(result.LineAmount.toLocaleString());
			$('#FromCity').val(result.FromCity).trigger("change");
			$('#ToCity').val(result.ToCity).trigger("change");
			$("#LineGlobalDimension1Code").val(result.LineGlobalDimension1Code).trigger("change");
			$("#LineGlobalDimension2Code").val(result.LineGlobalDimension2Code).trigger("change");
			$("#LineShortcutDimension3Code").val(result.LineShortcutDimension3Code).trigger("change");
			$("#LineShortcutDimension4Code").val(result.LineShortcutDimension4Code).trigger("change");
			$("#LineShortcutDimension5Code").val(result.LineShortcutDimension5Code).trigger("change");
			$("#LineShortcutDimension6Code").val(result.LineShortcutDimension6Code).trigger("change");
			$("#LineShortcutDimension7Code").val(result.LineShortcutDimension7Code).trigger("change");
			$("#LineShortcutDimension8Code").val(result.LineShortcutDimension8Code).trigger("change");

			$("#ImprestLineModal").modal('show');
		},
		error: OnError
	});
	return false;
}

//Delete imprest line
function DeleteImprestLine(LineNo, DocumentNo) {
	var ans = confirm("Are you sure you want to delete this Line?");
	if (ans) {
		$.ajax({
			url: AJAXUrls.DeleteImprestLine,
			type: "POST",
			dataType: "json",
			data: { LineNo: LineNo, DocumentNo: DocumentNo },
			cache: false,
			success: function (result) {
				LoadImprestLines(DocumentNo);
				GetImprestAmount(DocumentNo);
			},
			error: function (errormessage) {
				//alert(errormessage.responseText);
				alert("Error");
			}
		});
	}
}

function ValidateImprestHeader() {

	//Clear imprest line modal
	ClearImprestLineModal();

	var isValid = true;
	if ($('#No').val().trim() == "") {
		$('#No').css('border-color', 'Red');
		isValid = false;
	}
	else {
		$('#No').css('border-color', 'lightgrey');
	}

	if ($('#GlobalDimension1Code').val().trim() == "") {
		$("#GlobalDimension1CodeError").show();
		isValid = false;
	}
	else {
		$('#errorGlobalDimension1Code').hide();
	}

	return isValid;
}

function ValidateImprestLine() {
	var isValid = true;
	var label = "";
	if ($("#ImprestCode").val().trim() == "") {
		$("#ImprestCodeError").text("Imprest code cannot be empty.");
		isValid = false;
	}
	else {
		$("#ImprestCodeError").text("");
	}

	if ($("#LineDescription").val().trim() == "") {
		$("#LineDescriptionError").text("Imprest line description cannot be empty.");
		isValid = false;
	}
	else {
		$("#LineDescriptionError").text("");
	}

	return isValid;
}

//Load Funds Claim lines
function LoadFundsClaimLines(DocumentNo) {
	$.ajax({
		url: AJAXUrls.GetFundClaimLines,
		type: "GET",
		dataType: "json",
		data: { DocumentNo: DocumentNo },
		cache: false,
		success: function (imprestLines) {
			var rows = "";
			$.each(imprestLines, function (i, imprestLine) {
				rows += "<tr>";
				rows += "<td>" + imprestLine.ImprestCode + "</td>";
				rows += "<td>" + imprestLine.LineDescription + "</td>";
				rows += "<td>" + imprestLine.LineAmount.toLocaleString() + "</td>";
				rows += "<td>" + imprestLine.FromCity + "</td>";
				rows += "<td>" + imprestLine.ToCity + "</td>";
				rows += '<td><a href="#" onclick="return EditFundsClaimLine(' + imprestLine.LineNo + ',\'' + imprestLine.DocumentNo + '\');">Edit</a> | <a href="#" onclick="DeleteFundsClaimLine(' + imprestLine.LineNo + ',\'' + imprestLine.DocumentNo + '\')">Delete</a></td>';
				rows += "</tr>";
				$("#FundsClaimLineTbl tbody").html(rows);
			});
			$("#AjaxLoader").css("display", "none");
			$("#FundsClaimLineTbl").css("display", "block");
		},
		error: function (xhr, status, thrownError) {
			rows += "<tr>";
			rows += "<td class='text-danger text-center' colspan='8'>Unable to load the funds claim lines.</td>";
			rows += "</tr>";
			$("#FundsClaimLineTbl tbody").html(rows);
			$("#AjaxLoader").css("display", "none");
			$("#FundsClaimLineTbl").css("display", "block");
		}
	});
}

//Load Funds Claim lines View
function LoadFundsClaimLinesView(DocumentNo) {
	$.ajax({
		url: AJAXUrls.GetFundClaimLines,
		type: "GET",
		dataType: "json",
		data: { DocumentNo: DocumentNo },
		cache: false,
		success: function (imprestLines) {
			var rows = "";
			$.each(imprestLines, function (i, imprestLine) {
				rows += "<tr>";
				rows += "<td>" + imprestLine.ImprestCode + "</td>";
				rows += "<td>" + imprestLine.LineDescription + "</td>";
				rows += "<td>" + imprestLine.LineAmount.toLocaleString() + "</td>";
				rows += "<td>" + imprestLine.FromCity + "</td>";
				rows += "<td>" + imprestLine.ToCity + "</td>";
				rows += '<td><a href="#" onclick="return ViewFundsClaimLine(' + imprestLine.LineNo + ',\'' + imprestLine.DocumentNo + '\');">View</a> </td>';
				rows += "</tr>";
				$("#FundsClaimLineTbl tbody").html(rows);
			});
			$("#AjaxLoader").css("display", "none");
			$("#FundsClaimLineTbl").css("display", "block");
		},
		error: function (xhr, status, thrownError) {
			rows += "<tr>";
			rows += "<td class='text-danger text-center' colspan='8'>Unable to load the funds claim lines.</td>";
			rows += "</tr>";
			$("#FundsClaimLineTbl tbody").html(rows);
			$("#AjaxLoader").css("display", "none");
			$("#FundsClaimLineTbl").css("display", "block");
		}
	});
}

//Create Funds Claim line   
function CreateFundsClaimLine() {

	var documentNo = $("#No").val();

	var validLine = ValidateFundsClaimLine();
	if (validLine == false) {
		return false;
	}

	var FundClaimLineObj = {
		DocumentNo: documentNo,
		ImprestCode: $("#ImprestCode").val(),
		LineDescription: $("#LineDescription").val(),
		LineAmount: $("#LineAmount").val(),
		FromCity: $("#FromCity").val(),
		ToCity: $("#ToCity").val(),
		LineGlobalDimension1Code: $("#LineGlobalDimension1Code").val(),
		LineGlobalDimension2Code: $("#LineGlobalDimension2Code").val(),
		LineShortcutDimension3Code: $("#LineShortcutDimension3Code").val(),
		LineShortcutDimension4Code: $("#LineShortcutDimension4Code").val(),
		LineShortcutDimension5Code: $("#LineShortcutDimension5Code").val(),
		LineShortcutDimension6Code: $("#LineShortcutDimension6Code").val(),
		LineShortcutDimension7Code: $("#LineShortcutDimension7Code").val(),
		LineShortcutDimension8Code: $("#LineShortcutDimension8Code").val()
	};

	$.ajax({
		url: AJAXUrls.CreateFundClaimLine,
		type: "POST",
		dataType: "json",
		contentType: "application/json",
		data: JSON.stringify(FundClaimLineObj),
		cache: false,
		success: function (result) {
			LoadFundsClaimLines(documentNo);
			GetImprestAmount(documentNo);
			$("#FundsClaimLineModal").modal("hide");
		},
		error: function (xhr, errorType, exception) {
			alert(xhr.responseText);
		}
	});
}

//Edit Funds Claim line
function EditFundsClaimLine(LineNo, DocumentNo) {
	$.ajax({
		url: AJAXUrls.GetFundClaimLineByLineNo,
		type: "GET",
		dataType: "json",
		data: { LineNo: LineNo, DocumentNo: DocumentNo },
		cache: false,
		success: function (result) {
			$('#LineNo').val(result.LineNo);
			$('#DocumentNo').val(result.DocumentNo);
			$('#ImprestCode').val(result.ImprestCode).trigger("change");
			$('#LineDescription').val(result.LineDescription).trigger("change");
			$('#LineAmount').val(result.LineAmount);
			$('#FromCity').val(result.FromCity).trigger("change");
			$('#ToCity').val(result.ToCity).trigger("change");
			$('#LineGlobalDimension1Code').val(result.LineGlobalDimension1Code).trigger("change");
			$('#LineGlobalDimension2Code').val(result.LineGlobalDimension2Code).trigger("change");
			$('#LineShortcutDimension3Code').val(result.LineShortcutDimension3Code).trigger("change");
			$('#LineShortcutDimension4Code').val(result.LineShortcutDimension4Code).trigger("change");
			$('#LineShortcutDimension5Code').val(result.LineShortcutDimension5Code).trigger("change");
			$('#LineShortcutDimension6Code').val(result.LineShortcutDimension6Code).trigger("change");
			$('#LineShortcutDimension7Code').val(result.LineShortcutDimension7Code).trigger("change");
			$('#LineShortcutDimension8Code').val(result.LineShortcutDimension8Code).trigger("change");

			$("#FundsClaimLineModal").modal("show");
			$("#CreateFundsClaimLineBtn").hide();
			$("#ModifyFundsClaimLineBtn").show();
		},
		error: OnError
	});
	return false;
}

//Modify Funds Claim line
function ModifyFundsClaimLine() {
	var documentNo = $("#No").val();

	var validLine = ValidateFundsClaimLine();
	if (validLine == false) {
		return false;
	}

	var FundClaimLineObj = {
		LineNo: $("#LineNo").val(),
		DocumentNo: documentNo,
		ImprestCode: $("#ImprestCode").val(),
		LineDescription: $("#LineDescription").val(),
		LineAmount: $("#LineAmount").val(),
		FromCity: $("#FromCity").val(),
		ToCity: $("#ToCity").val(),
		LineGlobalDimension1Code: $("#GlobalDimension1Code").val(),
		LineGlobalDimension2Code: $("#LineGlobalDimension2Code").val(),
		LineShortcutDimension3Code: $("#LineShortcutDimension3Code").val(),
		LineShortcutDimension4Code: $("#LineShortcutDimension4Code").val(),
		LineShortcutDimension5Code: $("#LineShortcutDimension5Code").val(),
		LineShortcutDimension6Code: $("#LineShortcutDimension6Code").val(),
		LineShortcutDimension7Code: $("#LineShortcutDimension7Code").val(),
		LineShortcutDimension8Code: $("#LineShortcutDimension8Code").val()
	};
	$.ajax({
		url: AJAXUrls.ModifyFundClaimLine,
		type: "POST",
		dataType: "json",
		contentType: "application/json",
		data: JSON.stringify(FundClaimLineObj),
		cache: false,
		success: function (result) {
			LoadFundsClaimLines(documentNo);
			GetImprestAmount(documentNo);
			$("#FundsClaimLineModal").modal("hide");

			ClearImprestLineModal();
		},
		error: OnError
	});
}

//Delete Funds Claim Line
function DeleteFundsClaimLine(LineNo, DocumentNo) {
	var ans = confirm("Are you sure you want to delete this Line?");
	if (ans) {
		$.ajax({
			url: AJAXUrls.DeleteFundClaimLine,
			type: "POST",
			dataType: "json",
			data: { LineNo: LineNo, DocumentNo: DocumentNo },
			cache: false,
			success: function (result) {
				LoadFundsClaimLines(DocumentNo);
				GetImprestAmount(DocumentNo);
			},
			error: function (errormessage) {
				alert("Error");
			}
		});
	}
}

//Validate Funds Claim Line
function ValidateFundsClaimLine() {
	var isValid = true;
	var label = "";
	if ($("#ImprestCode").val().trim() == "") {
		$("#ImprestCodeError").text("Imprest code cannot be empty.");
		isValid = false;
	}
	else {
		$("#ImprestCodeError").text("");
	}

	if ($("#LineDescription").val().trim() == "") {
		$("#LineDescriptionError").text("Imprest line description cannot be empty.");
		isValid = false;
	}
	else {
		$("#LineDescriptionError").text("");
	}

	if ($.isNumeric($("#LineAmount").val())) {
		$("#LineAmountError").text("");
	} else {
		("#LineAmountError").text("Imprest line amount must be numeric.");
		isValid = false;
	}

	if (($("#LineAmount").val() <= 0) || ($("#LineAmount").val().trim() == "")) {
		$("#LineAmountError").text("Imprest line amount cannot be less or equal to zero.");
		isValid = false;
	}
	else {
		$("#LineAmountError").text("");
	}
	return isValid;
}

//Clear imprest line modal
function ClearImprestLineModal() {
	$("#LineNo").val(0);
	$("#DocumentNo").val("");
	$("#ImprestCode").val("").trigger("change");
	$("#LineDescription").val("");
	$("#LineAmount").val(0);
	$("#Days").val(0);
	$("#FromCity").val("").trigger("change");
	$("#ToCity").val("").trigger("change");
	$("#LineGlobalDimension1Code").val("").trigger("change");
	$("#LineGlobalDimension2Code").val("").trigger("change");
	$("#LineShortcutDimension3Code").val("").trigger("change");
	$("#LineShortcutDimension4Code").val("").trigger("change");
	$("#LineShortcutDimension5Code").val("").trigger("change");
	$("#LineShortcutDimension6Code").val("").trigger("change");
	$("#LineShortcutDimension7Code").val("").trigger("change");
	$("#LineShortcutDimension8Code").val("").trigger("change");

	$("#CreateImprestLineBtn").show();
	$("#ModifyImprestLineBtn").hide();

	$("#CreateFundsClaimLineBtn").show();
	$("#ModifyFundsClaimLineBtn").hide();

	$("#ImprestCodeError").text("");
	$("#LineDescriptionError").text("");
	$("#DaysError").text("");
	$("#LineGlobalDimension1CodeError").text("");
	$("#LineGlobalDimension2CodeError").text("");
	$("#LineShortcutDimension3CodeError").text("");
	$("#LineShortcutDimension4CodeError").text("");
	$("#LineShortcutDimension5CodeError").text("");
	$("#LineShortcutDimension6CodeError").text("");
	$("#LineShortcutDimension7CodeError").text("");
	$("#LineShortcutDimension8CodeError").text("");
}

//Load Imprest Documents
function LoadImprestDocuments(DocumentNo) {
	$.ajax({
		url: AJAXUrls.GetPortalDocuments,
		type: "GET",
		dataType: "json",
		data: { DocumentNo: DocumentNo },
		cache: false,
		success: function (imprestUploadedDocuments) {
			var rows = "";
			$.each(imprestUploadedDocuments, function (i, imprestUploadedDocument) {
				rows += "<tr>";
				rows += "<td>" + imprestUploadedDocument.DocumentDescription + "</td>";
				if (imprestUploadedDocument.DocumentAttached) {
					rows += "<td>" + "<input type='checkbox' name='DocumentAttached' value='DocumentAttached' checked disabled>" + "</td>";
				} else {
					rows += "<td>" + "<input type='checkbox' name='DocumentAttached' value='DocumentAttached' disabled>" + "</td>";
				}

				rows += '<td><a href="#" onclick="return EditImprestDocument(\'' + DocumentNo + '\',\'' + imprestUploadedDocument.DocumentCode + '\');"><i class="fa fa-paperclip" aria-hidden="true">Attach Document</i></a></td>';
				rows += "</tr>";
			});

			$("#AjaxLoader").css("display", "none");
			$("#ApplicationDocumentsTbl tbody").html(rows);
			$("#ApplicationDocumentsTbl").css("display", "block");
		},
		error: function (xhr, status, error) {

		}
	});
}

//Load Imprest Documents View
function LoadImprestDocumentsView(DocumentNo) {
	$.ajax({
		url: AJAXUrls.GetPortalDocuments,
		type: "GET",
		dataType: "json",
		data: { DocumentNo: DocumentNo },
		cache: false,
		success: function (imprestUploadedDocuments) {
			var rows = "";
			$.each(imprestUploadedDocuments, function (i, imprestUploadedDocument) {
				rows += "<tr>";
				rows += "<td>" + imprestUploadedDocument.DocumentDescription + "</td>";
				if (imprestUploadedDocument.DocumentAttached) {
					rows += "<td>" + "<input type='checkbox' name='DocumentAttached' value='DocumentAttached' checked disabled>" + "</td>";
				} else {
					rows += "<td>" + "<input type='checkbox' name='DocumentAttached' value='DocumentAttached' disabled>" + "</td>";
				}

				rows += '<td><a href="#" onclick="return ViewImprestDocument(\'' + DocumentNo + '\',\'' + imprestUploadedDocument.DocumentCode + '\');"><i class="" aria-hidden="true">View</i></a></td>';
				rows += "</tr>";
			});

			$("#AjaxLoader").css("display", "none");
			$("#ApplicationDocumentsTbl tbody").html(rows);
			$("#ApplicationDocumentsTbl").css("display", "block");
		},
		error: function (xhr, status, error) {

		}
	});
}

//Edit imprest document
function EditImprestDocument(DocumentNo, DocumentCode) {

	ResetImprestDocumentModal();

	$.ajax({
		url: AJAXUrls.GetPortalDocumentLink,
		type: "GET",
		dataType: "json",
		data: { DocumentNo: DocumentNo, DocumentCode: DocumentCode },
		cache: false,
		success: function (applicationDocument) {
			$("#DocumentNo").val(applicationDocument.DocumentNo);
			$("#DocumentCode").val(applicationDocument.DocumentCode);
			$("#DocumentDescription").val(applicationDocument.DocumentDescription);
			$('#errorMessage').hide();
			$("#ApplicationDocumentModal").modal("show");
			$("#UploadApplicationDocumentBtn").show();
		},
		error: function (xhr, status, error) {

		}
	});
	return false;
}

//Reset imprest document Line
function ResetImprestDocumentModal() {
	$("#ApplicationDocumentFile").val("");
	Ladda.stopAll();
}

//Upload imprest document
function UploadImprestDocument() {
	var DocumentNo = $("#DocumentNo").val();
	var DocumentCode = $("#DocumentCode").val();
	var DocumentDescription = $("#DocumentDescription").val();

	var filebase = $("#ApplicationDocumentFile").get(0);
	var files = filebase.files;

	var form = $('ApplicationDocumentForm')[0];
	var frmData = new FormData();

	frmData.append("DocumentNo", DocumentNo);
	frmData.append("DocumentCode", DocumentCode);
	frmData.append("DocumentDescription", DocumentDescription);

	frmData.append(files[0].name, files[0]);

	//Block UI
	$.blockUI();

	$.ajax({
		url: AJAXUrls.UploadDocumentLink,
		type: "POST",
		data: frmData,
		dataType: 'json',
		contentType: false,
		processData: false,
		enctype: "multipart/form-data",
		async: true,
		cache: false,
		success: function (result) {
			$('#txtMessage').html(result.message);
			if (result.success) {
				$('#ApplicationDocumentModal').modal('hide');
				$('#errorMessage').hide();
				LoadImprestDocuments(DocumentNo);
				$.unblockUI();
			} else {
				$('#errorMessage').html(result.message);
				$('#errorMessage').show();
				$.unblockUI();
			}

			Ladda.stopAll();
		}

	});
}

function OnError(xhr, errorType, exception) {
	var responseText;
	$("#dialog").html("");
	try {
		responseText = jQuery.parseJSON(xhr.responseText);
		$("#dialog").append("<div><b>" + errorType + " " + exception + "</b></div>");
		$("#dialog").append("<div><u>Exception</u>:<br /><br />" + responseText.ExceptionType + "</div>");
		$("#dialog").append("<div><u>StackTrace</u>:<br /><br />" + responseText.StackTrace + "</div>");
		$("#dialog").append("<div><u>Message</u>:<br /><br />" + responseText.Message + "</div>");
		alert(responseText.Message);
	} catch (e) {
		responseText = xhr.responseText;
		$("#dialog").html(responseText);
	}
	$("#dialog").dialog({
		title: "jQuery Exception Details",
		width: 700,
		buttons: {
			Close: function () {
				$(this).dialog('close');
			}
		}
	});
}