function InitializeLeaveApplication() {
	var dateToday = '01/01/2019';

	$("#LeaveStartDate").datepicker({
		dateFormat: "dd/mm/yy",
		changeMonth: true,
		changeYear: true,
		minDate: dateToday
	});

	AddLeaveApplicationDropDownListSearch();

	AddOnChangeEvents();
}

function AddLeaveApplicationDropDownListSearch() {
	$("#SubstituteEmployeeNo").select2({
		placeholder: $("#SubstituteEmployeeNoLbl").text(),
		allowClear: true
	});

	$("#ResponsibilityCenter").select2({
		placeholder: $("#ResponsibilityCenterLbl").text(),
		allowClear: true
	});

}

function AddOnChangeEvents() {

	$("#LeaveType").change(function () {
		$("#LeaveStartDate").val('');
		$("#DaysApplied").val('');
		$("#LeaveEndDate").val('');
		$("#LeaveReturnDate").val('');
		InsertLeaveApplicationDocuments($(this).val());
	});

	//On Change Days Applied
	$("#DaysApplied").blur(function () {
		var DaysApplied = $(this).val();
		var EmployeeNo = $("#EmployeeNo").val();
		var LeaveType = $("#LeaveType").val();
		var LeaveStartDate = $("#LeaveStartDate").val();

		if (Math.floor(DaysApplied) == DaysApplied && $.isNumeric(DaysApplied)) {
			//if ($("#DaysApplied").val() > $("#LeaveBalance").val()) {
			//    $("#DaysAppliedError").text("The applied leave days. requested cannot be more than leave balance. ");
			//    isValid = false;
			//} else {
			//    $("#DaysAppliedError").text("");

			//}
			//Get Leave End Date
			$.ajax({
				url: AJAXUrls.GetLeaveEndDate,
				type: 'GET',
				data: { employeeNo: EmployeeNo, leaveType: LeaveType, leaveStartDate: LeaveStartDate, daysApplied: DaysApplied },
				dataType: 'json',
				success: function (response) {
					$("#LeaveEndDate").val(response);
				},
				error: function (xhr, reason, ex) {
					ShowDialogBox('Error', reason, 'Ok', '', 'GoToLogin', null);
					$("#LeaveType").val('');
					$("#LeaveStartDate").val('');
					$("#DaysApplied").val('');
				}
			});

			//Get Leave Return Date
			$.ajax({
				url: AJAXUrls.GetLeaveReturnDate,
				type: 'GET',
				data: { employeeNo: EmployeeNo, leaveType: LeaveType, leaveStartDate: LeaveStartDate, daysApplied: DaysApplied },
				dataType: 'json',
				success: function (response) {
					$("#LeaveReturnDate").val(response);
				},
				error: function (xhr, reason, ex) {
					ShowDialogBox('Error', reason, 'Ok', '', 'GoToLogin', null);
					$("#LeaveType").val('');
					$("#LeaveStartDate").val('');
					$("#DaysApplied").val('');
				}
			});
		}

	});
}

function ValidateLeaveApplication() {
	var isValid = true;
	var label = "";
	if ($("#DaysApplied").val().trim() == "") {
		$("#DaysAppliedError").text("Applied Days. cannot be empty ");
		isValid = false;
	}
	else {
		$("#DaysAppliedError").text("");
	}

	if ($.isNumeric($("#DaysApplied").val())) {
		$("#DaysAppliedError").text("");
	} else {
		("#DaysAppliedError").text("Applied Days. must be numeric.");
		isValid = false;
	}

	if (($("#DaysApplied").val() <= 0) || ($("#DaysApplied").val().trim() == "")) {
		$("#DaysAppliedrror").text("Applied Days. cannot be less or equal to zero.");
		isValid = false;
	}
	else {
		$("#DaysAppliedError").text("");
	}

	if ($("#LeaveBalance").val() == 0) {
		$("#DaysAppliedError").text("Your remaining leave days is zero. You do not qualify to apply for leave. ");
		isValid = false;
	} else {
		$("#DaysAppliedError").text("");
	}

	return isValid;
}

//insert leave application document
function InsertLeaveApplicationDocuments(LeaveType) {
	var LeaveApplicationNo = $("#No").val();
	$.ajax({
		url: AJAXUrls.InsertLeaveApplicationDocuments,
		type: "GET",
		dataType: "json",
		data: { LeaveApplicationNo: LeaveApplicationNo, LeaveType: LeaveType },
		cache: false,
		success: function (result) {
			LoadLeaveApplicationDocuments(LeaveApplicationNo);
		},
		error: function (errormessage) {
			//alert(" " + EmployeeLoan + "  exists for employee no. " + employeeNo + ", finalize on this loan application before creating a new one.");
		}
	});
}

//Load Leave Application Document
function LoadLeaveApplicationDocuments(LeaveApplicationNo) {
	$.ajax({
		url: AJAXUrls.LoadLeaveApplicationDocuments,
		type: "GET",
		dataType: "json",
		data: { LeaveApplicationNo: LeaveApplicationNo },
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

				rows += '<td><a href="#" onclick="return EditLeaveApplicationDocument(\'' + LeaveApplicationNo + '\',\'' + result.DocumentCode + '\');"><i class="fa fa-paperclip" aria-hidden="true"></i></a></td>';
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

//Load Leave Application Document View
function LoadLeaveApplicationDocumentsView(LeaveApplicationNo) {
	$.ajax({
		url: AJAXUrls.GetApplicationDocuments,
		type: "GET",
		dataType: "json",
		data: { LeaveApplicationNo: LeaveApplicationNo },
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

			$("#ApplicationAjaxLoader").css("display", "none");

			$("#ApplicationDocumentsTbl").css("display", "block");
		},
		error: function (xhr, status, error) {

		}
	});
}

//Edit Loan Application Document
function EditLeaveApplicationDocument(LeaveApplicationNo, DocumentCode) {

	//Clear link path
	ResetLeaveApplicationDocumentModal();

	$.ajax({
		url: AJAXUrls.GetLeaveApplicationDocumentByLineNo,
		type: "GET",
		dataType: "json",
		data: { LeaveApplicationNo: LeaveApplicationNo, DocumentCode: DocumentCode },
		cache: false,
		success: function (result) {
			$("#DocumentNo").val(result.DocumentNo);
			$("#DocumentCode").val(result.DocumentCode);
			$("#DocumentDescription").val(result.DocumentDescription);
			$('#errorMessage').hide();
			$("#ApplicationDocumentModal").modal("show");
			$("#UploadLeaveApplicationDocumentBtn").show();
		},
		error: function (xhr, status, error) {

		}
	});
	return false;
}

//Upload Leave Application Document
function UploadLeaveApplicationDocument() {

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
		url: AJAXUrls.UploadLeaveApplicationDocument,
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
				LoadLeaveApplicationDocuments(DocumentNo);
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

//Reset Leave Application Document Path
function ResetLeaveApplicationDocumentModal() {
	$("#ApplicationDocumentFile").val("");
	Ladda.stopAll();
}