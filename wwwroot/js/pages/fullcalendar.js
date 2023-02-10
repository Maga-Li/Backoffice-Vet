"use strict";
moment.locale('pt');

$(document).ready(function () {
    var events = [];
    var selectedEvent = null;
    FetchEventAndRenderCalendar();

    function FetchEventAndRenderCalendar() {
        events = [];
        $.ajax({
            type: "GET",
            url: "/Home/GetEvents",
            dataType: "Json",
            success: function (data) {
                $.each(data.result, function (i, v) {
                    events.push({
                        eventID: v.id,
                        title: v.title,
                        description: v.description,
                        start: moment(v.start).format("YYYY-MM-DDTHH:mm:ss"),
                        allDay: false
                    });
                })
                GenerateCalendar(events);
            },

            error: function (error) {
                console.log("Erro!");
            }
        })
    }

    function GenerateCalendar(events) {
        $('#calendar').fullCalendar('destroy');
        $('#calendar').fullCalendar({
            locale: 'pt-PT',
            contentHeight: 800,
            defaultDate: new Date(),
            timeFormat: 'HH:mm',
            slotLabelFormat: 'HH:mm',
            allDayText: 'Dia todo',
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,basicWeek,basicDay,agenda'
            },
            buttonText: {
                today: "Hoje",
                month: "Mês",
                week: "Semana",
                day: "Dia"
            },

            columnHeaderFormat: {
                month: 'ddd',
                week: 'ddd d/M',
                day: 'dddd d/M'
            },

            nowIndicator: true,
            eventLimit: true,
            eventColor: '#655dd0;',
            eventTextColor: '#FFFFFF;',
            events: events,
            eventClick: function (calEvent) {
                selectedEvent = calEvent;
                var $description = $('<ul />').css({ "list-style-type": "square", "padding-left": "20px" });
                $description.append($('<li />').html('<strong>Veterinário: </strong>' + calEvent.title));
                $description.append($('<li />').html('<strong>Data/Hora: </strong>' + calEvent.start.format("dddd, DD MMM YYYY HH:mm")).css("padding-top", "7px"));
                $description.append($('<li/>').html('<strong>Motivo: </strong>' + calEvent.description).css("padding-top", "7px"));
                $('#myModal #pDetails').empty().html($description);

                $('#myModal').modal('show');
                $(".js-edit").attr("href", "/Appointment/Edit?idAppointment=" + calEvent.eventID);
            },

            selectable: true,
            select: function (start) {
                selectedEvent = {
                    idAppointment: 0,
                    idAnimal: 0,
                    idVeterinarian: 0,
                    schedule: start,
                    observations: '',
                    price: 0.0,
                    idMotive: 0,
                    idPriority: 0
                };
                openAddEditForm();
                $('#calendar').fullCalendar('unselect');
            },

            editable: false
        })
    }

    $('#btnDelete').on("click", function () {
        swal({
            title: 'Tem a certeza?',
            text: 'Uma vez eliminado, não é possível recuperar os dados!',
            icon: 'warning',
            buttons: true,
            dangerMode: true,
        })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    type: "POST",
                    url: '/Home/DeleteEvent',
                    data: { 'idAppointment': selectedEvent.eventID },
                    success: function (data) {
                        if (data.status) {
                            //Refresh the calender
                            $('#myModal').modal('hide');
                            FetchEventAndRenderCalendar();

                            let appointByDayObj = $('.js-appointmentCountByDay');
                            let sumByDayAppoint = Number(appointByDayObj.text());
                            appointByDayObj.text(sumByDayAppoint - 1);

                            let appointObj = $('.js-appointmentCount');
                            let sumAppoint = Number(appointObj.text());
                            appointObj.text(sumAppoint - 1);

                            swal('Consulta eliminada!', {
                                icon: 'success',
                            });
                        }
                    },
                    error: function () {
                        console.log("Erro!");
                    }
                })
            } else {
                swal('Dados guardados sem alterações!');
            }
        });

        return false;

    })


    $('#dateTimeSchedule').datetimepicker({
        locale: "pt",
       /* format: "DD/MM/YYYY HH:mm",*/
        toolbarPlacement: "bottom",
        showClear: true,
        showClose: true,
        icons: {
            time: "fa-regular fa-clock",
            up: "fa-solid fa-chevron-up",
            down: "fa-solid fa-chevron-down",
            previous: "fa-solid fa-chevron-left",
            next: "fa-solid fa-chevron-right",
            clear: "fa-solid fa-trash-can",
            close: "fa-solid fa-circle-xmark"
        },
        tooltips: {
            clear: "Limpar",
            close: "Fechar",
            selectTime: "Escolher hora",

        }
    });

    function openAddEditForm() {
        if (selectedEvent != null) {
            $('#idAppointment').val(selectedEvent.idAppointment);
            $('#idAnimal').val(selectedEvent.idAnimal);
            $('#idVeterinarian').val(selectedEvent.idVeterinarian);
            $('#dateTimeSchedule').val(selectedEvent.schedule.format('DD/MM/YYYY HH:mm'));
            $('#txtObservation').val(selectedEvent.description);
            $('#decPrice').val(selectedEvent.price);
            $('#idMotive').val(selectedEvent.idMotive);
            $('#idPriority').val(selectedEvent.idPriority);
        }
        $('#myModal').modal('hide');
            $('#myModalSave').modal();
    }

    $('#btnSave').on("click", function (e) {
        e.preventDefault();
        if ($('#idAnimal').val() == 0) {
            swal('É necessário inserir o animal');
            return;
        }

        if ($('#idVeterinarian').val() == 0) {
            swal('É necessário inserir o médico veterinário');
            return;
        }

        if ($('#dateTimeSchedule').val().trim() == "") {
            swal('É necessário inserir a data/hora da consulta');
            return;
        }

        if ($('#idMotive').val() == 0) {
            swal('É necessário inserir o motivo da consulta');
            return;
        }

        if ($('#idPriority').val() == 0) {
            swal('É necessário inserir a prioridade da consulta');
            return;
        }

        var data = {
            IdAppointment: $('#idAppointment').val(),
            IdAnimal: $('#idAnimal').val(),
            IdVeterinarian: $('#idVeterinarian').val(),
            Schedule: $('#dateTimeSchedule').val().trim(),
            Observations: $('#txtObservation').val().trim(),
            Price: $('#decPrice').val(),
            IdMotive: $('#idMotive').val(),
            IdPriority: $('#idPriority').val(),

        }
        SaveEvent(data);
    })

    function SaveEvent(data) {
        $.ajax({
            type: "POST",
            url: '/Home/SaveEvent',
            data: data,
            success: function (data) {
                if (data.status) {
                    $('#myModalSave').modal('hide');
                    FetchEventAndRenderCalendar();

                    let appointByDayObj = $('.js-appointmentCountByDay');
                    let sumByDayAppoint = Number(appointByDayObj.text());
                    appointByDayObj.text(sumByDayAppoint + 1);

                    let appointObj = $('.js-appointmentCount');
                    let sumAppoint = Number(appointObj.text());
                    appointObj.text(sumAppoint + 1);
                }
            },
            error: function () {
                console.log("Erro!");
            }
        })
    }
})