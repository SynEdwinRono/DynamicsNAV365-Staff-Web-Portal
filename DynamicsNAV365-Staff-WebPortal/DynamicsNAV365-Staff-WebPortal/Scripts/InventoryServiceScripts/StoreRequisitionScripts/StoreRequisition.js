//Initialize store requisitions
function InitializeStoreRequisition() {
	var dateToday = new Date();
	$("#RequiredDate").datepicker({
		dateFormat: "dd/mm/yy",
		changeMonth: true,
		changeYear: true,
		minDate: dateToday
	});

	var itemNo = "";
	var uom = "";
	var locationCode = "";

	//Onchange events
	$("#ItemNo").change(function () {
		$("#Inventory").val(0);
		$("#Quantity").val(0);
		$("#QuantityError").text("");

		itemNo = $(this).val();
		GetItemUOMs(itemNo);
	});

	$("#UOM").change(function () {
		$("#Inventory").val(0);
		$("#Inventory").css("background-color", "White");
		$("#Quantity").val(0);
		$("#QuantityError").text("");

		itemNo = $("#ItemNo").val().trim();
		uom = $(this).val().trim();
		if (itemNo != "") {
			if ($("#LineLocationCode").val().trim() != "") {
				locationCode = $("#LineLocationCode").val().trim();
				GetAvailableInventory(itemNo, uom, locationCode);
			}
		} else {
			$("#ItemNoError").text("Item No. cannot be empty.");
			$("#UOM").val("");
		}

	});

	$("#LineLocationCode").change(function () {
		$("#Inventory").val(0);
		$("#Inventory").css("background-color", "White");
		$("#Quantity").val(0);
		$("#QuantityError").text("");

		itemNo = $("#ItemNo").val().trim();
		uom = $("#UOM").val().trim();
		locationCode = $(this).val().trim();
		if ((itemNo != "") && (uom != "") && (locationCode != "")) {
			GetAvailableInventory(itemNo, uom, locationCode);
		} else {
			if (itemNo == "") {
				$("#ItemNoError").text("Item No. cannot be empty.");
			}
			if (uom == "") {
				$("#UOMError").text("UOM cannot be empty.");
			}
			if (locationCode == "") {
				$("#LineLocationCodeError").text("Location Code cannot be empty.");
			}
		}
	});
	//End onchange events

	//Add dropdown search
	AddStoreRequisitionDropDownListSearch();
	//End add dropdown search
}

//AddStoreRequisitionDropDownListSearch
function AddStoreRequisitionDropDownListSearch() {
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

	$("#LineLocationCode").select2({
		placeholder: $("#LineLocationCodeLbl").text(),
		allowClear: true
	});
}

//Load store requisition lines
function LoadStoreRequisitionLines(DocumentNo) {
	$.ajax({
		url: AJAXUrls.GetStoreRequisitionLines,
		type: "GET",
		dataType: "json",
		data: { DocumentNo: DocumentNo },
		cache: false,
		success: function (storeRequisitionLines) {
			var rows = "";
			$.each(storeRequisitionLines, function (i, storeRequisitionLine) {
				rows += "<tr>";
				rows += "<td>" + storeRequisitionLine.ItemNo + "</td>";
				rows += "<td>" + storeRequisitionLine.ItemDescription + "</td>";
				rows += "<td>" + storeRequisitionLine.LineLocationCode + "</td>";
				rows += "<td>" + storeRequisitionLine.UOM + "</td>";
				rows += "<td>" + storeRequisitionLine.Inventory + "</td>";
				rows += "<td>" + storeRequisitionLine.Quantity + "</td>";
				rows += '<td><a href="#" onclick="return EditStoreRequisitionLine(' + storeRequisitionLine.LineNo + ',\'' + storeRequisitionLine.DocumentNo + '\');">Edit</a> | <a href="#" onclick="DeleteStoreRequisitionLine(' + storeRequisitionLine.LineNo + ',\'' + storeRequisitionLine.DocumentNo + '\')">Delete</a></td>';
				rows += "</tr>";
				$("#StoreRequisitionLineTbl tbody").html(rows);
			});
			$("#AjaxLoader").css("display", "none");
			$("#StoreRequisitionLineTbl").css("display", "block");
		},
		error: OnError
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
			$("#UOM").html(options);
		},
		error: OnError
	});
}

//LoadStoreRequisitionLinesView
function LoadStoreRequisitionLinesView(DocumentNo) {
	$.ajax({
		url: AJAXUrls.GetStoreRequisitionLines,
		type: "GET",
		dataType: "json",
		data: { DocumentNo: DocumentNo },
		cache: false,
		success: function (storeRequisitionLines) {
			var rows = "";
			$.each(storeRequisitionLines, function (i, storeRequisitionLine) {
				rows += "<tr>";
				rows += "<td>" + storeRequisitionLine.ItemNo + "</td>";
				rows += "<td>" + storeRequisitionLine.ItemDescription + "</td>";
				rows += "<td>" + storeRequisitionLine.LineLocationCode + "</td>";
				rows += "<td>" + storeRequisitionLine.UOM + "</td>";
				rows += "<td>" + storeRequisitionLine.Inventory + "</td>";
				rows += "<td>" + storeRequisitionLine.Quantity + "</td>";
				rows += "<td><a href='javascript:void(0);' onclick='return ViewStoreRequisitionLine(" + storeRequisitionLine.LineNo + ",\'" + storeRequisitionLine.DocumentNo + "\')';>View</a></td>";
				rows += "</tr>";
				$("#StoreRequisitionLineTbl tbody").html(rows);
			});
			$("#AjaxLoader").css("display", "none");
			$("#StoreRequisitionLineTbl").css("display", "block");
		},
		error: OnError
	});
}

//Create store requisition line   
function CreateStoreRequisitionLine() {
	var documentNo = $("#No").val();

	var validLine = ValidateStoreRequisitionLine();
	if (validLine == false) {
		return false;
	}

	var StoreRequisitionLineObj = {
		DocumentNo: documentNo,
		ItemNo: $("#ItemNo").val(),
		ItemDescription: $("#ItemDescription").val(),
		UOM: $("#UOM").val(),
		LineLocationCode: $("#LineLocationCode").val(),
		Inventory: $("#Inventory").val(),
		Quantity: $("#Quantity").val(),
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
		url: AJAXUrls.CreateStoreRequisitionLine,
		type: "POST",
		dataType: "json",
		contentType: "application/json",
		data: JSON.stringify(StoreRequisitionLineObj),
		cache: false,
		success: function (result) {
			LoadStoreRequisitionLines(documentNo);
			$("#StoreRequisitionLineModal").modal("hide");
		},
		error: function (xhr, errorType, exception) {
			alert(xhr.responseText);
		}
	});
}

//Edit store requisition line
function EditStoreRequisitionLine(LineNo, DocumentNo) {
	$.ajax({
		url: AJAXUrls.GetStoreRequisitionLineByLineNo,
		type: "GET",
		dataType: "json",
		data: { LineNo: LineNo, DocumentNo: DocumentNo },
		cache: false,
		success: function (result) {
			$("#LineNo").val(result.LineNo);
			$("#DocumentNo").val(result.DocumentNo);
			$("#ItemNo").val(result.ItemNo).trigger("change.select2");
			$("#ItemDescription").val(result.ItemDescription);
			$("#UOM").val(result.UOM).trigger("change.select2");
			$("#LineLocationCode").val(result.LineLocationCode).trigger("change.select2");
			$("#Inventory").val(result.Inventory);
			$("#Quantity").val(result.Quantity);
			$("#LineGlobalDimension1Code").val(result.LineGlobalDimension1Code);
			$("#LineGlobalDimension2Code").val(result.LineGlobalDimension2Code).trigger("change.select2");
			$("#LineShortcutDimension3Code").val(result.LineShortcutDimension3Code).trigger("change.select2");
			$("#LineShortcutDimension4Code").val(result.LineShortcutDimension4Code).trigger("change.select2");
			$("#LineShortcutDimension5Code").val(result.LineShortcutDimension5Code).trigger("change.select2");
			$("#LineShortcutDimension6Code").val(result.LineShortcutDimension6Code).trigger("change.select2");
			$("#LineShortcutDimension7Code").val(result.LineShortcutDimension7Code).trigger("change.select2");
			$("#LineShortcutDimension8Code").val(result.LineShortcutDimension8Code).trigger("change.select2");

			$("#StoreRequisitionLineModal").modal("show");
			$("#CreateStoreRequisitionLineBtn").hide();
			$("#ModifyStoreRequisitionLineBtn").show();
		},
		error: OnError
	});
	return false;
}

//Modify store requisition line
function ModifyStoreRequisitionLine() {
	var documentNo = $("#No").val();

	var validLine = ValidateStoreRequisitionLine();
	if (validLine == false) {
		return false;
	}
	var storeRequisitionLineObj = {
		LineNo: $("#LineNo").val(),
		DocumentNo: documentNo,
		ItemNo: $("#ItemNo").val(),
		ItemDescription: $("#ItemDescription").val(),
		UOM: $("#UOM").val(),
		LineLocationCode: $("#LineLocationCode").val(),
		Inventory: $("#Inventory").val(),
		Quantity: $("#Quantity").val(),
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
		url: AJAXUrls.ModifyStoreRequisitionLine,
		type: "POST",
		dataType: "json",
		contentType: "application/json",
		data: JSON.stringify(storeRequisitionLineObj),
		cache: false,
		success: function (result) {
			LoadStoreRequisitionLines(documentNo);
			$("#StoreRequisitionLineModal").modal("hide");

			ClearStoreRequisitionLineModal();
		},
		error: OnError
	});
}

//View store requisition line
function ViewStoreRequisitionLine(LineNo, DocumentNo) {
	$.ajax({
		url: AJAXUrls.GetStoreRequisitionLineByLineNo,
		type: "GET",
		dataType: "json",
		data: { LineNo: LineNo, DocumentNo: DocumentNo },
		cache: false,
		success: function (result) {
			$("#LineNo").val(result.LineNo);
			$("#DocumentNo").val(result.DocumentNo);
			$("#ItemNo").val(result.ItemNo);
			$("#ItemDescription").val(result.ItemDescription);
			$("#LineLocationCode").val(result.LineLocationCode);
			$("#UOM").val(result.UOM);
			$("#Inventory").val(result.Inventory);
			$("#Quantity").val(result.Quantity);
			$("#LineGlobalDimension1Code").val(result.LineGlobalDimension1Code);
			$("#LineGlobalDimension2Code").val(result.LineGlobalDimension2Code);
			$("#LineShortcutDimension3Code").val(result.LineShortcutDimension3Code);
			$("#LineShortcutDimension4Code").val(result.LineShortcutDimension4Code);
			$("#LineShortcutDimension5Code").val(result.LineShortcutDimension5Code);
			$("#LineShortcutDimension6Code").val(result.LineShortcutDimension6Code);
			$("#LineShortcutDimension7Code").val(result.LineShortcutDimension7Code);
			$("#LineShortcutDimension8Code").val(result.LineShortcutDimension8Code);

			$('#StoreRequisitionLineModal').modal('show');
		},
		error: OnError
	});
	return false;
}

//Delete store requisition line
function DeleteStoreRequisitionLine(LineNo, DocumentNo) {
	var ans = confirm("Are you sure you want to delete this Line?");
	if (ans) {
		$.ajax({
			url: AJAXUrls.DeleteStoreRequisitionLine,
			type: "POST",
			dataType: "json",
			data: { LineNo: LineNo, DocumentNo: DocumentNo },
			cache: false,
			success: function (result) {
				LoadStoreRequisitionLines(DocumentNo);
			},
			error: function (errormessage) {
				alert("Error");
			}
		});
	}
}

//Get available inventory
function GetAvailableInventory(ItemNo, UOM, LocationCode) {
	var availableInventory = 0;
	var requestedQuantity = $("#Quantity").val();
	$.ajax({
		url: AJAXUrls.GetAvailableInventory,
		type: "GET",
		dataType: "json",
		data: { ItemNo: ItemNo, UOM: UOM, LocationCode: LocationCode },
		cache: false,
		success: function (result) {
			availableInventory = result.AvailableInventory.toLocaleString();
			$("#Inventory").val(availableInventory);
		},
		error: OnError
	});

	//Onblur Quality
	$("#Quantity").blur(function () {
		//Validate Quantity Requested
		$.ajax({
			url: AJAXUrls.ValidateQuantityRequested,
			type: "GET",
			dataType: "json",
			data: { ItemNo: ItemNo, UOM: UOM, LocationCode: LocationCode, Quantity: $(this).val() },
			cache: false,
			success: function (result) {
				if (result.success === false) {
					$("#QuantityError").text(result.message);
					isValid = false;
					return isValid;
				}

				else {
					$("#QuantityError").text("");
				}
			}
		});

	});
}

//Validate Quantity Requested
function ValidateQuantityRequested(ItemNo, UOM, LocationCode, Quantity) {
	//Onblur Quality
	$("#Quantity").blur(function () {

		//Validate Quantity Requested
		$.ajax({
			url: AJAXUrls.ValidateQuantityRequested,
			type: "GET",
			dataType: "json",
			data: { ItemNo: ItemNo, UOM: UOM, LocationCode: LocationCode, Quantity: $(this).val() },
			cache: false,
			success: function (result) {
				if (result.success === false) {
					$("#QuantityError").text(result.message);
					isValid = false;
					return isValid;
				}

				else {
					$("#QuantityError").text("");
				}
			}
		});

	});
}

//Get store requisition amount
function GetStoreRequisitionAmount(DocumentNo) {
	var storeRequisitionAmount = 0;
	$.ajax({
		url: AJAXUrls.GetStoreRequisitionAmount,
		type: "GET",
		dataType: "json",
		data: { DocumentNo: DocumentNo },
		cache: false,
		success: function (result) {
			storeRequisitionAmount = result.Amount.toLocaleString();
			$("#Amount").val(storeRequisitionAmount);
		},
		error: OnError
	});
}

//ValidateStoreRequisition
function ValidateStoreRequisition() {
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
	//Clear store requisition line modal
	ClearStoreRequisitionLineModal();

	return isValid;
}

//ValidateStoreRequisitionLine
function ValidateStoreRequisitionLine() {
	var isValid = true;
	var label = "";
	if ($("#ItemNo").val().trim() == "") {
		$("#ItemNoError").text("Item No. cannot be empty.");
		isValid = false;
	}
	else {
		$("#ItemNoError").text("");
	}

	if ($("#LineLocationCode").val().trim() == "") {
		$("#LineLocationCodeError").text("Location Code cannot be empty.");
		isValid = false;
	}
	else {
		$("#LineLocationCodeError").text("");
	}

	if ($("#UOM").val().trim() == "") {
		$("#UOMError").text("UOM cannot be empty.");
		isValid = false;
	}
	else {
		$("#UOMError").text("");
	}

	if ($.isNumeric($("#Quantity").val())) {
		$("#QuantityError").text("");
	} else {
		("#QuantityError").text("Quantity must be numeric.");
		isValid = false;
	}

	//if (($("#Quantity").val() <= 0) || ($("#Quantity").val().trim() == "")) {
	//    $("#QuantityError").text("Quantity cannot be less or equal to zero.");
	//    isValid = false;
	//}
	//else {
	//    $("#QuantityError").text("");
	//}

	//if ($("#Quantity").val() > $("#Inventory").val()) {
	//    $("#QuantityError").text("The quantity requested cannot be more than the available inventory. ");
	//    isValid = false;
	//}else{
	//    $("#QuantityError").text("");
	//}

	//if ($("#LineGlobalDimension2Code").val().trim() == "") {
	//    label = $("#LineGlobalDimension2CodeLbl").text();
	//    $("#LineGlobalDimension2CodeError").text(label + " cannot be empty.");
	//    isValid = false;
	//}
	//else {
	//    $("#LineGlobalDimension2CodeError").text("");
	//}

	//if ($("#LineShortcutDimension3Code").val().trim() == "") {
	//    label = $("#LineShortcutDimension3CodeLbl").text();
	//    $("#LineShortcutDimension3CodeError").text(label + " cannot be empty.");
	//    isValid = false;
	//}
	//else {
	//    $("#LineShortcutDimension3CodeError").text("");
	//}

	//if ($("#LineShortcutDimension4Code").val().trim() == "") {
	//    label = $("#LineShortcutDimension4CodeLbl").text();
	//    $("#LineShortcutDimension4CodeError").text(label + " cannot be empty.");
	//    isValid = false;
	//}
	//else {
	//    $("#LineShortcutDimension4CodeError").text("");
	//}

	//if ($("#LineShortcutDimension5Code").val().trim() == "") {
	//    label = $("#LineShortcutDimension5CodeLbl").text();
	//    $("#LineShortcutDimension5CodeError").text(label + " cannot be empty.");
	//    isValid = false;
	//}
	//else {
	//    $("#LineShortcutDimension5CodeError").text("");
	//}

	//if ($("#LineShortcutDimension6Code").val().trim() == "") {
	//    label = $("#LineShortcutDimension6CodeLbl").text();
	//    $("#LineShortcutDimension6CodeError").text(label + " cannot be empty.");
	//    isValid = false;
	//}
	//else {
	//    $("#LineShortcutDimension6CodeError").text("");
	//}
	return isValid;
}

//Clear store requisition line modal
function ClearStoreRequisitionLineModal() {
	$("#LineNo").val(0);
	$("#DocumentNo").val("");
	$("#ItemNo").val("").trigger("change.select2");
	$("#ItemDescription").val("").trigger("change");
	$("#LineLocationCode").val("").trigger("change");
	$("#UOM").val("");
	$("#Inventory").val(0);
	$("#Quantity").val(0);
	$("#LineGlobalDimension1Code").val("").trigger("change");
	$("#LineGlobalDimension2Code").val("").trigger("change");
	$("#LineShortcutDimension3Code").val("").trigger("change");
	$("#LineShortcutDimension4Code").val("").trigger("change");
	$("#LineShortcutDimension5Code").val("").trigger("change");
	$("#LineShortcutDimension6Code").val("").trigger("change");
	$("#LineShortcutDimension7Code").val("").trigger("change");
	$("#LineShortcutDimension8Code").val("").trigger("change");

	$("#CreateStoreRequisitionLineBtn").show();
	$("#ModifyStoreRequisitionLineBtn").hide();

	$("#ItemNoError").text("");
	$("#ItemDescriptionError").text("");
	$("#LineLocationCodeError").text("");
	$("#UOMError").text("");
	$("#InventoryError").text("");
	$("#QuantityError").text("");
	$("#LineGlobalDimension1CodeError").text("");
	$("#LineGlobalDimension2CodeError").text("");
	$("#LineShortcutDimension3CodeError").text("");
	$("#LineShortcutDimension4CodeError").text("");
	$("#LineShortcutDimension5CodeError").text("");
	$("#LineShortcutDimension6CodeError").text("");
	$("#LineShortcutDimension7CodeError").text("");
	$("#LineShortcutDimension8CodeError").text("");
}

//on error
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