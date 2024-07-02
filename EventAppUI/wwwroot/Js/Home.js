var eventDataforCard = [];
var eventDetailId = sessionStorage.getItem('eventDetailId') || "";
var locationId = sessionStorage.getItem('locationId') || "";
var staffId = sessionStorage.getItem('staffId') || "";
console.log(page);
$(document).ready(function () {
    
    function loadEventData() {
        $.ajax({
            url: 'https://localhost:44354/api/event/getallevent',
            type: 'GET',
            success: function (data) {
                console.log("I'm in");
                console.log(data);
                eventDataforCard = data;  //updating global variable 
                buildAllCards(data);

            },
            error: function (error) {
                console.log(error);
            }

        })
    }

    function buildAllCards(eventdata) {
        console.log("card building..");
        //console.log(eventdata);
        console.log(eventDataforCard);
        var eventsDiv = $('#eventList');
        eventsDiv.empty()

        $.each(eventdata, function (index, event) {
            console.log(`from for each:${JSON.stringify(event)}`);
            let built = buildCard(event);
            eventsDiv.append(built);
        });

    }

    function buildCard(event) {
        console.log(`single card build:${event}`);
        let card = `
                            <div class="card d-flex flex-row card-shadow  justify-content-evenly  m-3">
                            <!--Left-->
                            <div class="card-body w-25 m-2 p-3 ">
                                                <span class="badge bg-success d-inline"> ${new Date(event.eventDate).toLocaleString('en-US', { month: 'long' })} Event </span>
                                                                        <p class=" m-1"> <i class="bi bi-calendar3 me-2"></i> <b> ${new Date(event.eventDate).toLocaleString('en-US', { day: 'numeric', month: 'short', year: 'numeric' })}</b></p>
                                                <p class=" m-1"> <i class="bi bi-clock  me-2"></i> ${new Date(event.eventDate).toLocaleString('en-US', { hour: '2-digit', minute: '2-digit', hour12: 'true' })} </p>
                            </div>
                            <!--Middle-->
                            <div class="card-body border-0 w-50 m-2 p-3">
                                <h3>${event.eventName}</h3>
                                <p>${event.eventDescription} </p>

                            </div>
                            <!--Right-->
                            <div class="card-body  w-25 m-2 p-3 d-flex justify-content-center align-items-center">
                               <button class="btn btn-warning edit me-2" data-eventid="${event.eventId}">Edit</button>

                                <button class="btn btn-primary details" data-eventid="${event.eventId}" data-eventname="${event.eventName}">Details</button>
                                <button type="button" id="delete-${event.eventId}" class="btn btn-danger deleteEvent ms-2" data-id =${event.eventId} data-name="${event.eventName}" ><i class="bi bi-trash-fill"></i></button>
                                
                            </div>

                        </div>
                          `;
        return card;
    }
    //delete modal 
    function showModal(event) {
        console.log(event);

        var deleteModal = $('#deleteModal');
        console.log(deleteModal);

        var modalBody = `<p>Event Id : ${event.Id} </p>
                            <p>Event Name : ${event.Name} </p>
                    `;
        deleteModal.find('.modal-body').html(modalBody);
        deleteModal.modal('show');

        $("#confirmDelete").on("click", function () {
            console.log("confirm clicked");
            window.location.href = `/Home/DeleteEvent/${event.Id}`;
        })
    }
    function setEventLocationCrumpsLink() {
       
        $('#eventLocationCrum').attr("href", `/location/EventLocation/${eventDetailId}`);
    }
    function setEventStaffCrumpsLink() {
        $('#EventStaffsCrum').attr("href", `/location/StaffsInEvent/${eventDetailId}/${locationId}`);
    }
    function setTopicCrumpsLink() {
        $('#EventTopicsCrum').attr("href", `/location/TopicsInEvent/${eventDetailId}/${locationId}/${staffId}`);
    }
    switch (page) {
        case 'Events':
            loadEventData();

            $('#search').on('input', function () {
                let searchVal = $(this).val().trim().toLowerCase();
                console.log(searchVal);
                var filtered = eventDataforCard.filter(function (event) {
                    return event.eventName.toLowerCase().includes(searchVal) || event.eventDescription.toLowerCase().includes(searchVal);
                })
                buildAllCards(filtered);
            })
            //on clicking details in the home page-navigate to details page showing the location
            $(document).on('click', '.details', function () {
                eventDetailId = $(this).data('eventid');
                var eventname = $(this).data('eventname');
                console.log(eventDetailId);
                sessionStorage.setItem('eventDetailId', eventDetailId); // Stores in sessionStorage
                sessionStorage.setItem('eventName', eventname);
                window.location.href = `/location/EventLocation/${eventDetailId}`;
                
            });
            $(document).on('click', '.edit', function () {
                var eventId = $(this).data('eventid');
                window.location.href = `/Home/EditEvent/${eventId}`;

            })
            //on delete click
            $(document).on('click', '.deleteEvent', function () {
                var event = {
                    Id: $(this).data('id'),
                    Name: $(this).data('name')
                };

                console.log(event);
                showModal(event);
            });
            break;
        case 'EventLocation':
            setEventLocationCrumpsLink();
            var name = sessionStorage.getItem('eventName');
            $('#eventTitle').text(name +" Locations");
            //on clicking the row in the location table
            $(document).on('click', '.location-table-data', function () {
                locationId = $(this).data('locid');
                console.log(locationId);
                console.log(eventDetailId);
                sessionStorage.setItem('locationId', locationId);
                window.location.href = `/location/StaffsInEvent/${eventDetailId}/${locationId}`;

            });
            $(document).on('click', '#addLocation',function () {
                window.location.href = `https://localhost:44360/location/AddLocation/${eventDetailId}`;
            });
           
            break;
        case 'EventStaffs':
            setEventLocationCrumpsLink();
            setEventStaffCrumpsLink();
            var name = sessionStorage.getItem('eventName');
            $('#eventTitle').text(name + " Staffs");
            //on clicking row in staff table
            $(document).on('click', '.staff-table-row', function () {
                staffId = $(this).data('staffid');
                console.log(locationId);
                console.log(eventDetailId);
                console.log(staffId)
                sessionStorage.setItem('staffId', staffId);
                window.location.href = `/location/TopicsInEvent/${eventDetailId}/${locationId}/${staffId}`;
               
            })
            
            break;

        case 'TopicsInEvent':
            setEventLocationCrumpsLink();
            setEventStaffCrumpsLink();
            setTopicCrumpsLink();
            var name = sessionStorage.getItem('eventName');
            $('#eventTitle').text(name + " Topics");
            break;
    }
 



})