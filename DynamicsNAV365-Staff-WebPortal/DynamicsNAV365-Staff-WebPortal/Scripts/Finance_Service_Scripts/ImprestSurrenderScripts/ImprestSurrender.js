//Initialize Imprest Details
function InitializeImprestSurrenderScripts() {
	var dateToday = new Date(0);
	$("#ImprestDate").datepicker({
		dateFormat: "dd/mm/yy",
		changeMonth: true,
		changeYear: true,
		minDate: dateToday
	});

	AddOnChangeEvents();
	AddImprestSurrenderDropDownListSearch();

}
//On ChangeEvents 
function AddOnChangeEvents() {
	$("#ImprestNumber").change(function () {
		ValidateImprestSurrenderLines($(this).val());
	});
}
//Add Dropdown list search
function AddImprestSurrenderDropDownListSearch() {
	$("#ImprestSurrenderCode").select2({
		placeholder: $("#ImprestSurrenderCodeLbl").text(),
		allowClear: true
	});
}
//Validate imprest surrender lines
function ValidateImprestSurrenderLines(ImprestNo) {
	var DocumentNo = $("#No").val();
	$.ajax({
		url: AJAXUrls.ValidateImprestSurrenderLines,
		type: "GET",
		dataType: "json",
		data: { DocumentNo: DocumentNo, ImprestNo: ImprestNo },
		cache: false,
		success: function (result) {
			LoadImprestSurrenderLines(DocumentNo);
		},
		error: function (errormessage) {
			LoadImprestSurrenderLines(DocumentNo);
		}
	});
}
//Edit imprest surrender line
function EditImprestSurrenderLine(LineNo, DocumentNo) {
	$.ajax({
		url: AJAXUrls.GetImprestSurrenderLine,
		type: "GET",
		dataType: "json",
		data: { LineNo: LineNo, DocumentNo: DocumentNo },
		cache: false,
		success: function (result) {
			$('#LineNo').val(result.LineNo);
			$('#DocumentNo').val(result.DocumentNo);
			$('#ImprestSurrenderCode').val(result.ImprestSurrenderCode).trigger("change");
			$('#LineSurrenderDescription').val(result.LineSurrenderDescription).trigger("change");
			$('#LineActualAmount').val(result.LineActualAmount);
			$('#LineGlobalDimension1Code').val(result.LineGlobalDimension1Code).trigger("change");
			$('#LineGlobalDimension2Code').val(result.LineGlobalDimension2Code).trigger("change");
			$('#LineShortcutDimension3Code').val(result.LineShortcutDimension3Code).trigger("change");
			$('#LineShortcutDimension4Code').val(result.LineShortcutDimension4Code).trigger("change");
			$('#LineShortcutDimension5Code').val(result.LineShortcutDimension5Code).trigger("change");
			$('#LineShortcutDimension6Code').val(result.LineShortcutDimension6Code).trigger("change");
			$('#LineShortcutDimension7Code').val(result.LineShortcutDimension7Code).trigger("change");
			$('#LineShortcutDimension8Code').val(result.LineShortcutDimension8Code).trigger("change");

			$("#ImprestSurrenderLineModal").modal("show");
			$("#CreateImprestSurrenderLineBtn").hide();
			$("#ModifyImprestSurrenderLineBtn").show();
		},
		error: OnError
	});
	return false;
}
//Load imprest surrender requests
function LoadImprestSurrenders() {
	var rows = "";
	$("#AjaxLoader").css("display", "block");
	$("#ImprestSurrenderListTbl").css("display", "none");

	$.ajax({
		url: AJAXUrls.getImprestSurrenders,
		type: "GET",
		dataType: "json",
		cache: false,
		success: function (imprestSurrenders) {
			var editCardUrl = "";
			var viewCardUrl = "";
			$.each(imprestSurrenders, function (i, imprestSurrender) {
				editCardUrl = AJAXUrls.editImprestSurrender + imprestSurrender.No;
				viewCardUrl = AJAXUrls.viewImprestSurrender + imprestSurrender.No;
				rows += "<tr>";
				rows += "<td>" + imprestSurrender.No + "</td>";
				rows += "<td>" + imprestSurrender.DocumentDate + "</td>";
				rows += "<td>" + imprestSurrender.CurrencyCode + "</td>";
				rows += "<td>" + imprestSurrender.Amount.toLocaleString() + "</td>";
				rows += "<td>" + imprestSurrender.Description + "</td>";
				rows += "<td>" + imprestSurrender.GlobalDimension1Code + "</td>";
				rows += "<td>" + imprestSurrender.Status + "</td>";
				rows += "<td>" + "<a href='" + editCardUrl + "' class='btn btn-primary print-btn'><i class='fa fa-edit'></i>Edit</a></td>"
				rows += "<td>" + "<a href='" + viewCardUrl + "' class='btn btn-default print-btn'><i class='fa fa-search'></i>View</a></td>"
				rows += "</tr>";
				$("#ImprestSurrenderListTbl tbody").html(rows);
			});
			$("#AjaxLoader").css("display", "none");
			$("#ImprestSurrenderListTbl").css("display", "block");
		},
		error: function (xhr, status, thrownError) {
			rows += "<tr>";
			rows += "<td class='text-danger text-center' colspan='8'>Unable to load the imprest surrenders.</td>";
			rows += "</tr>";
			$("#ImprestSurrenderListTbl tbody").html(rows);
			$("#AjaxLoader").css("display", "none");
			$("#ImprestSurrenderListTbl").css("display", "block");
		}
	});

	$("#ImprestSurrenderListTbl").dataTable();
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
//Get imprest surrender amount
function GetImprestSurrenderAmount(DocumentNo) {
	var imprestSurrenderAmount = 0;
	$.ajax({
		url: AJAXUrls.GetImprestSurrenderAmount,
		type: "GET",
		dataType: "json",
		data: { DocumentNo: DocumentNo },
		cache: false,
		success: function (result) {
			imprestSurrenderAmount = result.Amount.toLocaleString();
			$("#ActualSpent").val(imprestSurrenderAmount);
		},
		error: OnError
	});
}
//Load imprest surrender lines
function LoadImprestSurrenderLines(DocumentNo) {
	$.ajax({
		url: AJAXUrls.GetImprestSurrenderLinesAjax,
		type: "GET",
		dataType: "json",
		data: { DocumentNo: DocumentNo },
		cache: false,
		success: function (imprestSurrenderLines) {
			var rows = "";
			$.each(imprestSurrenderLines, function (i, imprestSurrenderLine) {
				rows += "<tr>";
				rows += "<td>" + imprestSurrenderLine.ImprestSurrenderCode + "</td>";
				rows += "<td>" + imprestSurrenderLine.LineSurrenderDescription + "</td>";
				rows += "<td>" + imprestSurrenderLine.LineAmount.toLocaleString() + "</td>";
				rows += "<td>" + imprestSurrenderLine.LineActualAmount.toLocaleString() + "</td>";
				//rows += '<td><a href="javascript:void(0);" onclick="return EditImprestSurrenderLine(' + imprestSurrenderLine.LineNo + ',\'' + imprestSurrenderLine.DocumentNo + '\');">Edit</a> | <a href="#" onclick="DeleteImprestSurrenderLine(' + imprestSurrenderLine.LineNo + ',\'' + imprestSurrenderLine.DocumentNo + '\')">Delete</a></td>';
				rows += '<td><a href="javascript:void(0);" onclick="return EditImprestSurrenderLine(' + imprestSurrenderLine.LineNo + ',\'' + imprestSurrenderLine.DocumentNo + '\');"><i class="fa fa-edit" aria-hidden="true">Edit</i></a></td>';
				rows += "</tr>";
				$("#ImprestSurrenderLineTbl tbody").html(rows);
			});
			$("#AjaxLoader").css("display", "none");
			$("#ImprestSurrenderLineTbl").css("display", "block");
		},
		error: OnError
	});
}
//Load imprest surrender lines View
function LoadImprestSurrenderLinesView(DocumentNo) {
	$.ajax({
		url: AJAXUrls.GetImprestSurrenderLinesAjax,
		type: "GET",
		dataType: "json",
		data: { DocumentNo: DocumentNo },
		cache: false,
		success: function (imprestSurrenderLines) {
			var rows = "";
			$.each(imprestSurrenderLines, function (i, imprestSurrenderLine) {
				rows += "<tr>";
				rows += "<td>" + imprestSurrenderLine.ImprestSurrenderCode + "</td>";
				rows += "<td>" + imprestSurrenderLine.LineSurrenderDescription + "</td>";
				rows += "<td>" + imprestSurrenderLine.LineAmount.toLocaleString() + "</td>";
				rows += "<td>" + imprestSurrenderLine.LineActualAmount.toLocaleString() + "</td>";
				//rows += "<td>" + imprestSurrenderLine.LineGlobalDimension2Code + "</td>";
				//rows += "<td>" + imprestSurrenderLine.LineShortcutDimension3Code + "</td>";
				//rows += "<td>" + imprestSurrenderLine.LineShortcutDimension4Code + "</td>";
				//rows += "<td>" + imprestSurrenderLine.LineShortcutDimension5Code + "</td>";
				//rows += "<td>" + imprestSurrenderLine.LineShortcutDimension6Code + "</td>";
				rows += "<td><a href='javascript:void(0);' onclick='return ViewImprestSurrenderLine(" + imprestSurrenderLine.LineNo + ",\'" + imprestSurrenderLine.DocumentNo + "\')';>View</a></td>";
				rows += "</tr>";
				$("#ImprestSurrenderLineTbl tbody").html(rows);
			});
			$("#AjaxLoader").css("display", "none");
			$("#ImprestSurrenderLineTbl").css("display", "block");
		},
		error: OnError
	});
}
//Create imprest line   
function CreateImprestSurrenderLine() {
	var documentNo = $("#No").val();

	//var validLine = ValidateImprestSurrenderLine();
	//if (validLine == false) {
	//	return false;
	//}

	var ImprestSurrenderLineObj = {
		DocumentNo: documentNo,
		ImprestSurrenderCode: $("#ImprestSurrenderCode").val(),
		LineSurrenderDescription: $("#LineSurrenderDescription").val(),
		LineActualAmount: $("#LineActualAmount").val(),
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
		url: AJAXUrls.CreateImprestSurrenderLine,
		type: "POST",
		dataType: "json",
		contentType: "application/json",
		data: JSON.stringify(ImprestSurrenderLineObj),
		cache: false,
		success: function (result) {
			LoadImprestSurrenderLines(documentNo);
			//imprest Amount
			GetImprestAmount(documentNo);
			//Actual Spent
			GetImprestSurrenderAmount(documentNo);
			$("#ImprestSurrenderLineModal").modal("hide");
		},
		error: function (xhr, errorType, exception) {
			alert(xhr.responseText);
		}
	});
}
//Modify imprest surrender line
function ModifyImprestSurrenderLine() {

	var documentNo = $("#No").val();

	var ImprestSurrenderLineObj = {
		LineNo: $("#LineNo").val(),
		DocumentNo: documentNo,
		ImprestSurrenderCode: $("#ImprestSurrenderCode").val(),
		LineSurrenderDescription: $("#LineSurrenderDescription").val(),
		LineActualAmount: $("#LineActualAmount").val(),
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
		url: AJAXUrls.ModifyImprestSurrenderLine,
		type: "POST",
		dataType: "json",
		contentType: "application/json",
		data: JSON.stringify(ImprestSurrenderLineObj),
		cache: false,
		success: function (result) {
			LoadImprestSurrenderLines(documentNo);
			//imprest Amount
			GetImprestAmount(documentNo);
			//Actual Spent
			GetImprestSurrenderAmount(documentNo);
			$("#ImprestSurrenderLineModal").modal("hide");
		},
		error: function (xhr, errorType, exception) {
			alert(xhr.responseText);
		}
	});
}
//View imprest surrender line
function ViewImprestSurrenderLine(LineNo, DocumentNo) {
	$.ajax({
		url: AJAXUrls.GetImprestSurrenderLine,
		type: "GET",
		dataType: "json",
		data: { LineNo: LineNo, DocumentNo: DocumentNo },
		cache: false,
		success: function (result) {
			$('#LineNo').val(result.LineNo);
			$('#DocumentNo').val(result.DocumentNo);
			$('#ImprestSurrenderCode').val(result.ImprestSurrenderCode);
			$('#LineSurrenderDescription').val(result.LineSurrenderDescription);
			$('#LineAmount').val(result.LineAmount.toLocaleString());
			$('#LineActualAmount').val(result.LineAmount.toLocaleString());
			$('#LineGlobalDimension1Code').val(result.LineGlobalDimension1Code);
			$('#LineGlobalDimension2Code').val(result.LineGlobalDimension2Code);
			$('#LineShortcutDimension3Code').val(result.LineShortcutDimension3Code);
			$('#LineShortcutDimension4Code').val(result.LineShortcutDimension4Code);
			$('#LineShortcutDimension5Code').val(result.LineShortcutDimension5Code);
			$('#LineShortcutDimension6Code').val(result.LineShortcutDimension6Code);
			$('#LineShortcutDimension7Code').val(result.LineShortcutDimension7Code);
			$('#LineShortcutDimension8Code').val(result.LineShortcutDimension8Code);

			$('#ImprestLineModal').modal('show');
		},
		error: OnError
	});
	return false;
}
//Delete imprest surrender line
function DeleteImprestSurrenderLine(LineNo, DocumentNo) {
	var ans = confirm("Are you sure you want to delete this Line?");
	if (ans) {
		$.ajax({
			url: AJAXUrls.DeleteImprestSurrenderLine,
			type: "POST",
			dataType: "json",
			data: { LineNo: LineNo, DocumentNo: DocumentNo },
			cache: false,
			success: function (result) {
				LoadImprestSurrenderLines(DocumentNo);
				//imprest Amount
				GetImprestAmount(DocumentNo);
				//Actual Spent
				GetImprestSurrenderAmount(DocumentNo);
			},
			error: function (errormessage) {
				//alert(errormessage.responseText);
				alert("Error");
			}
		});
	}
}
//Validate Imprest Surrender Header
function ValidateImprestSurrenderHeader() {
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
	//Clear imprest surrender line modal
	ClearImprestSurrenderLineModal();

	return isValid;
}
//Validate Imprest Surrender Line
function ValidateImprestSurrenderLine() {
	var isValid = true;
	var label = "";
	if ($("#ImprestSurrenderCode").val().trim() == "") {
		$("#ImprestSurrenderCodeError").text("Imprest code cannot be empty.");
		isValid = false;
	}
	else {
		$("#ImprestSurrenderCodeError").text("");
	}

	if ($("#LineSurrenderDescription").val().trim() == "") {
		$("#LineSurrenderDescriptionError").text("Imprest surrender line description cannot be empty.");
		isValid = false;
	}
	else {
		$("#LineSurrenderDescriptionError").text("");
	}

	if ($.isNumeric($("#LineActualAmount").val())) {
		$("#LineActualAmountError").text("");
	} else {
		("#LineActualAmountError").text("Imprest surrender line amount must be numeric.");
		isValid = false;
	}

	if (($("#LineActualAmount").val() <= 0) || ($("#LineActualAmount").val().trim() == "")) {
		$("#LineActualAmountError").text("Imprest surrender line amount cannot be less or equal to zero.");
		isValid = false;
	}
	else {
		$("#LineAmountError").text("");
	}

	if ($("#LineGlobalDimension2Code").val().trim() == "") {
		label = $("#LineGlobalDimension2CodeLbl").text();
		$("#LineGlobalDimension2CodeError").text(label + " cannot be empty.");
		isValid = false;
	}
	else {
		$("#LineGlobalDimension2CodeError").text("");
	}

	if ($("#LineShortcutDimension3Code").val().trim() == "") {
		label = $("#LineShortcutDimension3CodeLbl").text();
		$("#LineShortcutDimension3CodeError").text(label + " cannot be empty.");
		isValid = false;
	}
	else {
		$("#LineShortcutDimension3CodeError").text("");
	}

	if ($("#LineShortcutDimension4Code").val().trim() == "") {
		label = $("#LineShortcutDimension4CodeLbl").text();
		$("#LineShortcutDimension4CodeError").text(label + " cannot be empty.");
		isValid = false;
	}
	else {
		$("#LineShortcutDimension4CodeError").text("");
	}

	if ($("#LineShortcutDimension5Code").val().trim() == "") {
		label = $("#LineShortcutDimension5CodeLbl").text();
		$("#LineShortcutDimension5CodeError").text(label + " cannot be empty.");
		isValid = false;
	}
	else {
		$("#LineShortcutDimension5CodeError").text("");
	}

	if ($("#LineShortcutDimension6Code").val().trim() == "") {
		label = $("#LineShortcutDimension6CodeLbl").text();
		$("#LineShortcutDimension6CodeError").text(label + " cannot be empty.");
		isValid = false;
	}
	else {
		$("#LineShortcutDimension6CodeError").text("");
	}
	return isValid;
}
//Clear imprest line modal
function ClearImprestSurrenderLineModal() {
	$("#LineNo").val(0);
	$("#DocumentNo").val("");
	$("#ImprestCode").val("").trigger("change");
	$("#LineDescription").val("");
	$("#LineAmount").val(0);
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

	$("#ImprestCodeError").text("");
	$("#LineDescriptionError").text("");
	$("#LineAmountError").text("");
	$("#LineGlobalDimension1CodeError").text("");
	$("#LineGlobalDimension2CodeError").text("");
	$("#LineShortcutDimension3CodeError").text("");
	$("#LineShortcutDimension4CodeError").text("");
	$("#LineShortcutDimension5CodeError").text("");
	$("#LineShortcutDimension6CodeError").text("");
	$("#LineShortcutDimension7CodeError").text("");
	$("#LineShortcutDimension8CodeError").text("");
}
//Load Imprest Surrender Remaining Amount
function LoadImprestSurrenderRemainingAmount(ImprestNo) {
	//Onblur Imprest Amount
	$("#ActualSpent").blur(function () {
		var imprestNo = $("#ImprestNo").val();
		var actualSpent = $(this).val();
		var balance = 0;
		//Get Imprest Remaining Amount
		$.ajax({
			url: AJAXUrls.GetImprestBalance,
			type: "GET",
			dataType: "json",
			data: { imprestNo: imprestNo, actualSpent: actualSpent },
			cache: false,
			success: function (result) {
				balance = result.Difference.toLocaleString();
				$("#Difference").val(balance);
				$("#Difference").css("background-color", "LightGray");
				$("#ActualSpent").css("background-color", "LightGray");
			},
			error: function (errormessage) {
				alert("Error");
			}
		});
	});
}
//Clear imprest surrender line modal
function ClearImprestSurrenderLineModal() {
	$("#LineNo").val(0);
	$("#DocumentNo").val("");
	$("#ImprestSurrenderCode").val("").trigger("change");
	$("#LineSurrenderDescription").val("");
	$("#LineActualAmount").val(0);
	$("#LineGlobalDimension1Code").val("").trigger("change");
	$("#LineGlobalDimension2Code").val("").trigger("change");
	$("#LineShortcutDimension3Code").val("").trigger("change");
	$("#LineShortcutDimension4Code").val("").trigger("change");
	$("#LineShortcutDimension5Code").val("").trigger("change");
	$("#LineShortcutDimension6Code").val("").trigger("change");
	$("#LineShortcutDimension7Code").val("").trigger("change");
	$("#LineShortcutDimension8Code").val("").trigger("change");

	$("#CreateImprestSurrenderLineBtn").show();
	$("#ModifyImprestSurrenderLineBtn").hide();
	$("#ModifyImprestLineBtn").hide();

	$("#ImprestSurrenderCodeError").text("");
	$("#LineSurrenderDescriptionError").text("");
	$("#LineActualAmountError").text("");
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
		url: AJAXUrls.GetPortalDocumentLinks,
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

				rows += '<td><a href="#" onclick="return EditImprestDocument(\'' + DocumentNo + '\',\'' + imprestUploadedDocument.DocumentCode + '\');"><i class="fa fa-paperclip" aria-hidden="true">Attach</i></a></td>';
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
		url: AJAXUrls.GetPortalDocumentLinks,
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
		url: AJAXUrls.GetPortalDocumentLinkByLineNo,
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
	var DocumentNo = $("#No").val();
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
		url: AJAXUrls.UploadPortalDocumentLink,
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
//error
function OnError(xhr, errorType, exception) {
	var responseText;
	$("#dialog").html("");
	try {
		responseText = jQuery.parseJSON(xhr.responseText);
		$("#dialog").append("<div><b>" + errorType + " " + exception + "</b></div>");
		$("#dialog").append("<div><u>Exception</u>:<br /><br />" + responseText.ExceptionType + "</div>");
		$("#dialog").append("<div><u>StackTrace</u>:<br /><br />" + responseText.StackTrace + "</div>");
		$("#dialog").append("<div><u>Message</u>:<br /><br />" + responseText.Message + "</div>");
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
