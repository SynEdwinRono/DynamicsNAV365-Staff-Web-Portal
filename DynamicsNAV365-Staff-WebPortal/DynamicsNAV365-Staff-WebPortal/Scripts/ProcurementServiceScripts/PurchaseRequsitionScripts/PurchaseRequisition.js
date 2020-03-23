//Initialize purchase requisitions
function InitializePurchaseRequisition() {
	var dateToday = new Date();
	$("#RequestedReceiptDate").datepicker({
		dateFormat: "dd/mm/yy",
		changeMonth: true,
		changeYear: true,
		minDate: dateToday
	});

	AddOnChangeEvents();
	//End onchange events

	//Add dropdown search
	AddPurchaseRequisitionDropDownListSearch();
	//End add dropdown search
}

function AddPurchaseRequisitionDropDownListSearch() {
	$("#CurrencyCode").select2({
		placeholder: $("#CurrencyCodeLbl").text(),
		allowClear: true
	});

	$("#ResponsibilityCenter").select2({
		placeholder: $("#ResponsibilityCenterLbl").text(),
		allowClear: true
	});

	$("#RequisitionType").select2({
		placeholder: $("#RequisitionTypeLbl").text(),
		allowClear: true
	});

	$("#RequisitionCode").select2({
		placeholder: $("#RequisitionCodeLbl").text(),
		allowClear: true
	});

	$("#LineLocationCode").select2({
		placeholder: $("#LineLocationCodeLbl").text(),
		allowClear: true
	});

	$("#LocationCode").select2({
		placeholder: $("#LocationCodeLbl").text(),
		allowClear: true
	});

	$("#ItemNo").select2({
		placeholder: $("#ItemNoLbl").text(),
		allowClear: true
	});

	$("#UOM").select2({
		placeholder: $("#UOMLbl").text(),
		allowClear: true
	});
}

function AddOnChangeEvents() {
	$("#RequisitionType").change(function () {

		if ($(this).val() == "Item") {
			GetPurchaseRequisitionItems($(this).val());
		}
		if ($(this).val() == "Fixed Asset") {
			GetPurchaseRequisitionFixedAssets($(this).val());
		}

		else {
			GetPurchaseRequisitionCodes($(this).val());
		}
	});
}

//Load item UOMs lines
function GetItemUOMs(ItemNo) {
	var options = "";
	options += "<option>";
	options += "";
	options += "</option>";

	$.ajax({
		url: AJAXUrls.GetItemUOMs,
		type: "GET",
		dataType: "json",
		data: { ItemNo: ItemNo },
		cache: false,
		success: function (itemUOMs) {
			var rows = "";
			$.each(itemUOMs, function (i, itemUOM) {
				options += "<option value='" + itemUOM.Code + "'>";
				options += itemUOM.Code;
				options += "</option>";
			});
			$("#RequisitionCode").html(options);
		},
		error: OnError
	});
}

//Load purchase requisition lines
function LoadPurchaseRequisitionLines(DocumentNo) {
	$.ajax({
		url: AJAXUrls.GetPurchaseRequisitionLines,
		type: "GET",
		dataType: "json",
		data: { DocumentNo: DocumentNo },
		cache: false,
		success: function (purchaseRequisitionLines) {
			var rows = "";
			$.each(purchaseRequisitionLines, function (i, purchaseRequisitionLine) {
				rows += "<tr>";
				rows += "<td>" + purchaseRequisitionLine.RequisitionType + "</td>";
				rows += "<td>" + purchaseRequisitionLine.RequisitionCode + "</td>";
				rows += "<td>" + purchaseRequisitionLine.LineDescription + "</td>";
				//rows += "<td>" + purchaseRequisitionLine.LineLocationCode + "</td>";
				rows += "<td>" + purchaseRequisitionLine.Quantity.toLocaleString() + "</td>";
				rows += "<td>" + purchaseRequisitionLine.UOM + "</td>";
				rows += "<td>" + purchaseRequisitionLine.UnitCost.toLocaleString() + "</td>";
				rows += "<td>" + purchaseRequisitionLine.TotalLineCost.toLocaleString() + "</td>";
				//rows += "<td>" + purchaseRequisitionLine.LineGlobalDimension2Code + "</td>";
				//rows += "<td>" + purchaseRequisitionLine.LineShortcutDimension3Code + "</td>";
				//rows += "<td>" + purchaseRequisitionLine.LineShortcutDimension4Code + "</td>";
				//rows += "<td>" + purchaseRequisitionLine.LineShortcutDimension5Code + "</td>";
				//rows += "<td>" + purchaseRequisitionLine.LineShortcutDimension6Code + "</td>";
				rows += '<td><a href="#" onclick="return EditPurchaseRequisitionLine(' + purchaseRequisitionLine.LineNo + ',\'' + purchaseRequisitionLine.DocumentNo + '\');">Edit</a> | <a href="#" onclick="DeletePurchaseRequisitionLine(' + purchaseRequisitionLine.LineNo + ',\'' + purchaseRequisitionLine.DocumentNo + '\')">Delete</a></td>';
				rows += "</tr>";
				$("#PurchaseRequisitionLineTbl tbody").html(rows);
			});
			$("#AjaxLoader").css("display", "none");
			$("#PurchaseRequisitionLineTbl").css("display", "block");
		},
		error: OnError
	});
}

//Load purchase requisition codes
function GetPurchaseRequisitionCodes(RequisitionType) {
	var options = "";
	options += "<option>";
	options += "";
	options += "</option>";

	$.ajax({
		url: AJAXUrls.GetPurchaseRequisitionCodes,
		type: "GET",
		dataType: "json",
		data: { RequisitionType: RequisitionType },
		cache: false,
		success: function (requisitionCodes) {
			var rows = "";
			$.each(requisitionCodes, function (i, requisitionCode) {
				options += "<option value='" + requisitionCode.Requisition_Code + "'>";
				options += requisitionCode.Requisition_Code;
				options += "</option>";
			});
			$("#RequisitionCode").html(options);
		},
		error: OnError
	});
}

//Load purchase requisition codes
function GetPurchaseRequisitionItems(RequisitionType) {
	var options = "";
	options += "<option>";
	options += "";
	options += "</option>";

	$.ajax({
		url: AJAXUrls.GetPurchaseRequisitionItems,
		type: "GET",
		dataType: "json",
		data: { RequisitionType: RequisitionType },
		cache: false,
		success: function (requisitionCodes) {
			var rows = "";
			$.each(requisitionCodes, function (i, requisitionCode) {
				options += "<option value='" + requisitionCode.No + "'>";
				options += requisitionCode.Description;
				options += "</option>";
			});
			$("#RequisitionCode").html(options);
		},
		error: OnError
	});
}

//Load purchase requisition codes
function GetPurchaseRequisitionFixedAssets(RequisitionType) {
	var options = "";
	options += "<option>";
	options += "";
	options += "</option>";

	$.ajax({
		url: AJAXUrls.GetPurchaseRequisitionFixedAssets,
		type: "GET",
		dataType: "json",
		data: { RequisitionType: RequisitionType },
		cache: false,
		success: function (requisitionCodes) {
			var rows = "";
			$.each(requisitionCodes, function (i, requisitionCode) {
				options += "<option value='" + requisitionCode.No + "'>";
				options += requisitionCode.Description;
				options += "</option>";
			});
			$("#RequisitionCode").html(options);
		},
		error: OnError
	});
}

function LoadPurchaseRequisitionLinesView(DocumentNo) {
	$.ajax({
		url: AJAXUrls.GetPurchaseRequisitionLines,
		type: "GET",
		dataType: "json",
		data: { DocumentNo: DocumentNo },
		cache: false,
		success: function (purchaseRequisitionLines) {
			var rows = "";
			$.each(purchaseRequisitionLines, function (i, purchaseRequisitionLine) {
				rows += "<tr>";
				rows += "<td>" + purchaseRequisitionLine.RequisitionType + "</td>";
				rows += "<td>" + purchaseRequisitionLine.RequisitionCode + "</td>";
				rows += "<td>" + purchaseRequisitionLine.LineDescription + "</td>";
				//rows += "<td>" + purchaseRequisitionLine.LineLocationCode + "</td>";
				rows += "<td>" + purchaseRequisitionLine.Quantity.toLocaleString() + "</td>";
				rows += "<td>" + purchaseRequisitionLine.UOM + "</td>";
				rows += "<td>" + purchaseRequisitionLine.UnitCost.toLocaleString() + "</td>";
				rows += "<td>" + purchaseRequisitionLine.TotalLineCost.toLocaleString() + "</td>";
				rows += "<td><a href='javascript:void(0);' onclick='return ViewPurchaseRequisitionLine(" + purchaseRequisitionLine.LineNo + ",\'" + purchaseRequisitionLine.DocumentNo + "\')';>View</a></td>";
				rows += "</tr>";
				$("#PurchaseRequisitionLineTbl tbody").html(rows);
			});
			$("#AjaxLoader").css("display", "none");
			$("#PurchaseRequisitionLineTbl").css("display", "block");
		},
		error: OnError
	});
}

//Create purchase requisition line   
function CreatePurchaseRequisitionLine() {
	var documentNo = $("#No").val();

	var validLine = ValidatePurchaseRequisitionLine();
	if (validLine == false) {
		return false;
	}

	var PurchaseRequisitionLineObj = {
		DocumentNo: documentNo,
		RequisitionType: $("#RequisitionType").val(),
		RequisitionCode: $("#RequisitionCode").val(),
		LineDescription: $("#LineDescription").val(),
		LineLocationCode: $("#LineLocationCode").val(),
		Quantity: $("#Quantity").val(),
		UOM: $("#UOM").val(),
		UnitCost: $("#UnitCost").val(),
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
		url: AJAXUrls.CreatePurchaseRequisitionLine,
		type: "POST",
		dataType: "json",
		contentType: "application/json",
		data: JSON.stringify(PurchaseRequisitionLineObj),
		cache: false,
		success: function (result) {
			LoadPurchaseRequisitionLines(documentNo);
			$("#PurchaseRequisitionLineModal").modal("hide");
		},
		error: function (xhr, errorType, exception) {
			alert(xhr.responseText);
		}
	});
}

//Edit purchase requisition line
function EditPurchaseRequisitionLine(LineNo, DocumentNo) {
	$.ajax({
		url: AJAXUrls.GetPurchaseRequisitionLineByLineNo,
		type: "GET",
		dataType: "json",
		data: { LineNo: LineNo, DocumentNo: DocumentNo },
		cache: false,
		success: function (result) {
			$("#LineNo").val(result.LineNo);
			$("#DocumentNo").val(result.DocumentNo);
			$("#RequisitionType").val(result.RequisitionType).trigger("change.select2");
			$("#RequisitionCode").val(result.RequisitionCode).trigger("change.select2");
			$("#LineDescription").val(result.LineDescription);
			$("#LineLocationCode").val(result.LineLocationCode).trigger("change.select2");
			$("#Quantity").val(result.Quantity);
			$("#UOM").val(result.UOM);
			$("#UnitCost").val(result.UnitCost);
			$("#TotalLineCost").val(result.TotalLineCost);
			$("#LineGlobalDimension1Code").val(result.LineGlobalDimension1Code);
			$("#LineGlobalDimension2Code").val(result.LineGlobalDimension2Code).trigger("change.select2");
			$("#LineShortcutDimension3Code").val(result.LineShortcutDimension3Code).trigger("change.select2");
			$("#LineShortcutDimension4Code").val(result.LineShortcutDimension4Code).trigger("change.select2");
			$("#LineShortcutDimension5Code").val(result.LineShortcutDimension5Code).trigger("change.select2");
			$("#LineShortcutDimension6Code").val(result.LineShortcutDimension6Code).trigger("change.select2");
			$("#LineShortcutDimension7Code").val(result.LineShortcutDimension7Code).trigger("change.select2");
			$("#LineShortcutDimension8Code").val(result.LineShortcutDimension8Code).trigger("change.select2");

			$("#PurchaseRequisitionLineModal").modal("show");
			$("#CreatePurchaseRequisitionLineBtn").hide();
			$("#ModifyPurchaseRequisitionLineBtn").show();
		},
		error: OnError
	});
	return false;
}

//Modify purchase requisition line
function ModifyPurchaseRequisitionLine() {
	var documentNo = $("#No").val();

	var validLine = ValidatePurchaseRequisitionLine();
	if (validLine == false) {
		return false;
	}
	var PurchaseRequisitionLineObj = {
		LineNo: $("#LineNo").val(),
		DocumentNo: documentNo,
		RequisitionType: $("#RequisitionType").val(),
		RequisitionCode: $("#RequisitionCode").val(),
		LineDescription: $("#LineDescription").val(),
		LineLocationCode: $("#LineLocationCode").val(),
		Quantity: $("#Quantity").val(),
		UOM: $("#UOM").val(),
		UnitCost: $("#UnitCost").val(),
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
		url: AJAXUrls.ModifyPurchaseRequisitionLine,
		type: "POST",
		dataType: "json",
		contentType: "application/json",
		data: JSON.stringify(PurchaseRequisitionLineObj),
		cache: false,
		success: function (result) {
			LoadPurchaseRequisitionLines(documentNo);
			$("#PurchaseRequisitionLineModal").modal("hide");

			ClearPurchaseRequisitionLineModal();
		},
		error: OnError
	});
}

//View purchase requisition line
function ViewPurchaseRequisitionLine(LineNo, DocumentNo) {
	$.ajax({
		url: AJAXUrls.GetPurchaseRequisitionLineByLineNo,
		type: "GET",
		dataType: "json",
		data: { LineNo: LineNo, DocumentNo: DocumentNo },
		cache: false,
		success: function (result) {
			$("#LineNo").val(result.LineNo);
			$("#DocumentNo").val(result.DocumentNo);
			$("#RequisitionType").val(result.RequisitionType);
			$("#RequisitionCode").val(result.RequisitionCode);
			$("#LineDescription").val(result.LineDescription);
			$("#LineLocationCode").val(result.LineLocationCode);
			$("#Quantity").val(result.Quantity);
			$("#UOM").val(result.UOM);
			$("#UnitCost").val(result.UnitCost);
			$("#TotalLineCost").val(result.TotalLineCost);
			$("#LineGlobalDimension1Code").val(result.LineGlobalDimension1Code);
			$("#LineGlobalDimension2Code").val(result.LineGlobalDimension2Code);
			$("#LineShortcutDimension3Code").val(result.LineShortcutDimension3Code);
			$("#LineShortcutDimension4Code").val(result.LineShortcutDimension4Code);
			$("#LineShortcutDimension5Code").val(result.LineShortcutDimension5Code);
			$("#LineShortcutDimension6Code").val(result.LineShortcutDimension6Code);
			$("#LineShortcutDimension7Code").val(result.LineShortcutDimension7Code);
			$("#LineShortcutDimension8Code").val(result.LineShortcutDimension8Code);

			$('#PurchaseRequisitionLineModal').modal('show');
		},
		error: OnError
	});
	return false;
}

//Delete purchase requisition line
function DeletePurchaseRequisitionLine(LineNo, DocumentNo) {
	var ans = confirm("Are you sure you want to delete this Line?");
	if (ans) {
		$.ajax({
			url: AJAXUrls.DeletePurchaseRequisitionLine,
			type: "POST",
			dataType: "json",
			data: { LineNo: LineNo, DocumentNo: DocumentNo },
			cache: false,
			success: function (result) {
				LoadPurchaseRequisitionLines(DocumentNo);
			},
			error: function (errormessage) {
				alert("Error");
			}
		});
	}
}

//Get purchase requisition amount
function GetPurchaseRequisitionAmount(DocumentNo) {
	var purchaseRequisitionAmount = 0;
	$.ajax({
		url: AJAXUrls.GetPurchaseRequisitionAmount,
		type: "GET",
		dataType: "json",
		data: { DocumentNo: DocumentNo },
		cache: false,
		success: function (result) {
			purchaseRequisitionAmount = result.Amount.toLocaleString();
			$("#Amount").val(purchaseRequisitionAmount);
		},
		error: OnError
	});
}

function ValidatePurchaseRequisition() {
	var isValid = true;
	if ($('#No').val().trim() == "") {
		$('#No').css('border-color', 'Red');
		isValid = false;
	}
	else {
		$('#No').css('border-color', 'lightgrey');
	}

	//if ($('#GlobalDimension1Code').val().trim() == "") {
	//    $("#GlobalDimension1CodeError").show();
	//    isValid = false;
	//}
	//else {
	//    $('#errorGlobalDimension1Code').hide();
	//}
	//Clear purchase requisition line modal
	ClearPurchaseRequisitionLineModal();

	return isValid;
}

function ValidatePurchaseRequisitionLine() {
	var isValid = true;
	var label = "";
	if ($("#RequisitionType").val().trim() == "") {
		$("#RequisitionTypeError").text("Requisition type cannot be empty.");
		isValid = false;
	}
	else {
		$("#RequisitionTypeError").text("");
	}

	if ($("#RequisitionCode").val().trim() == "") {
		$("#RequisitionCodeError").text("Requisition code cannot be empty.");
		isValid = false;
	}
	else {
		$("#RequisitionCodeError").text("");
	}

	if ($("#LineDescription").val().trim() == "") {
		$("#LineDescriptionError").text("Description cannot be empty.");
		isValid = false;
	}
	else {
		$("#LineDescriptionCodeError").text("");
	}

	if ($.isNumeric($("#Quantity").val())) {
		$("#QuantityError").text("");
	} else {
		("#QuantityError").text("Quantity must be numeric.");
		isValid = false;
	}

	if (($("#Quantity").val() <= 0) || ($("#Quantity").val().trim() == "")) {
		$("#QuantityError").text("Quantity cannot be less or equal to zero.");
		isValid = false;
	}
	else {
		$("#QuantityError").text("");
	}

	if ($("#UOM").val().trim() == "") {
		$("#UOMError").text("Unit of measure cannot be empty.");
		isValid = false;
	}
	else {
		$("#UOMError").text("");
	}

	if ($.isNumeric($("#UnitCost").val())) {
		$("#UnitCostError").text("");
	} else {
		("#UnitCostError").text("Unit cost must be numeric.");
		isValid = false;
	}

	if (($("#UnitCost").val() <= 0) || ($("#UnitCost").val().trim() == "")) {
		$("#UnitCostError").text("Unit cost cannot be less or equal to zero.");
		isValid = false;
	}
	else {
		$("#UnitCostError").text("");
	}

	return isValid;
}

//Clear purchase requisition line modal
function ClearPurchaseRequisitionLineModal() {
	$("#LineNo").val(0);
	$("#DocumentNo").val("");
	$("#RequisitionType").val("").trigger("change.select2");
	$("#RequisitionCode").val("").trigger("change");
	$("#LineDescription").val("").trigger("change");
	$("#LineLocationCode").val("").trigger("change");
	$("#Quantity").val(0);
	$("#UOM").val("");
	$("#UnitCost").val(0);
	$("#TotalLineCost").val(0);
	$("#LineGlobalDimension1Code").val("").trigger("change");
	$("#LineGlobalDimension2Code").val("").trigger("change");
	$("#LineShortcutDimension3Code").val("").trigger("change");
	$("#LineShortcutDimension4Code").val("").trigger("change");
	$("#LineShortcutDimension5Code").val("").trigger("change");
	$("#LineShortcutDimension6Code").val("").trigger("change");
	$("#LineShortcutDimension7Code").val("").trigger("change");
	$("#LineShortcutDimension8Code").val("").trigger("change");


	$("#CreatePurchaseRequisitionLineBtn").show();
	$("#ModifyPurchaseRequisitionLineBtn").hide();

	$("#RequisitionTypeError").text("");
	$("#RequisitionCodeError").text("");
	$("#LineDescriptionError").text("");
	$("#LineLocationCodeError").text("");
	$("#QuantityError").text("");
	$("#UOMError").text("");
	$("#UnitCostError").text("");
	$("#TotalLineCostError").text("");
	$("#LineGlobalDimension1CodeError").text("");
	$("#LineGlobalDimension2CodeError").text("");
	$("#LineShortcutDimension3CodeError").text("");
	$("#LineShortcutDimension4CodeError").text("");
	$("#LineShortcutDimension5CodeError").text("");
	$("#LineShortcutDimension6CodeError").text("");
	$("#LineShortcutDimension7CodeError").text("");
	$("#LineShortcutDimension8CodeError").text("");
}

//Load Purchase Requisition Document
function LoadPurchaseRequisitionDocuments(DocumentNo) {
	$.ajax({
		url: AJAXUrls.LoadPurchaseRequisitionDocuments,
		type: "GET",
		dataType: "json",
		data: { DocumentNo: DocumentNo },
		cache: false,
		success: function (results) {
			var rows = "";
			$.each(results, function (i, result) {
				rows += "<tr>";
				rows += "<td>" + result.DocumentDescription + "</td>";
				if (result.DocumentAttached) {
					rows += "<td>" + "<input type='checkbox' name='DocumentAttached' value='DocumentAttached' checked disabled>" + "</td>";
				} else {
					rows += "<td>" + "<input type='checkbox' name='DocumentAttached' value='DocumentAttached' disabled>" + "</td>";
				}

				rows += '<td><a href="#" onclick="return EditPurchaseRequisitionDocument(\'' + DocumentNo + '\',\'' + result.DocumentCode + '\');"><i class="fa fa-paperclip" aria-hidden="true">Attach Document</i></a></td>';
				rows += "</tr>";
			});
			$("#ApplicationDocumentsTbl tbody").html(rows);

			$("#AjaxLoader").css("display", "none");

			$("#ApplicationDocumentsTbl").css("display", "block");
		},
		error: function (xhr, status, error) {

		}
	});
}

//Load Purchase Requisition Document View
function LoadPurchaseRequisitionDocumentsView(DocumentNo) {
	$.ajax({
		url: AJAXUrls.LoadPurchaseRequisitionDocuments,
		type: "GET",
		dataType: "json",
		data: { DocumentNo: DocumentNo },
		cache: false,
		success: function (results) {
			var rows = "";
			$.each(results, function (i, result) {
				rows += "<tr>";
				rows += "<td>" + result.DocumentDescription + "</td>";
				if (result.DocumentAttached) {
					rows += "<td>" + "<input type='checkbox' name='DocumentAttached' value='DocumentAttached' checked disabled>" + "</td>";
				} else {
					rows += "<td>" + "<input type='checkbox' name='DocumentAttached' value='DocumentAttached' disabled>" + "</td>";
				}
				rows += "</tr>";
			});
			$("#ApplicationDocumentsTbl tbody").html(rows);

			$("#AjaxLoader").css("display", "none");

			$("#ApplicationDocumentsTbl").css("display", "block");
		},
		error: function (xhr, status, error) {

		}
	});
}

//Edit Purchase Requisition Document
function EditPurchaseRequisitionDocument(DocumentNo, DocumentCode) {

	//Clear link path
	ResetPurchaseRequisitionDocumentModal();

	$.ajax({
		url: AJAXUrls.GetPurchaseRequisitionDocumentByLineNo,
		type: "GET",
		dataType: "json",
		data: { DocumentNo: DocumentNo, DocumentCode: DocumentCode },
		cache: false,
		success: function (result) {
			$("#DocumentNo").val(result.DocumentNo);
			$("#DocumentCode").val(result.DocumentCode);
			$("#DocumentDescription").val(result.DocumentDescription);
			$('#errorMessage').hide();
			$("#ApplicationDocumentModal").modal("show");
			$("#UploadPurchaseRequisitionDocumentBtn").show();
		},
		error: function (xhr, status, error) {

		}
	});
	return false;
}

//Upload Purchase Requisition Document
function UploadPurchaseRequsitionDocuments() {

	//Assign values to this variables
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
		url: AJAXUrls.UploadPurchaseRequsitionAttachments,
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
				LoadPurchaseRequisitionDocuments(DocumentNo);
				$.unblockUI();
			} else {
				$('#errorMessage').html(result.message);
				$('#errorMessage').show();
				$.unblockUI();
			}
			Ladda.stopAll();
		},
		error: function (err) {
			$('#ApplicationDocumentModal').modal('show');
			$('#errorMessage').html(err.statusText);
			$('#errorMessage').show();
			Ladda.stopAll();
		}
	});
}

//Reset Purchase Requisition Document Path
function ResetPurchaseRequisitionDocumentModal() {
	$("#ApplicationDocumentFile").val("");
	Ladda.stopAll();
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
