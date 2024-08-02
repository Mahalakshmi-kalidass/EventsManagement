var eventDataforCard = [];
var eventDetailId = sessionStorage.getItem('eventDetailId') || "";
var locationId = sessionStorage.getItem('locationId') || "";
var staffId = sessionStorage.getItem('staffId') || "";
console.log(page);
$(document).ready(function () {
    
    function loadEventData(isEditable) {
        $.ajax({
            url: 'https://localhost:7239/api/event/getallevent',
            type: 'GET',
            success: function (data) {
                console.log("I'm in");
                console.log(data);
                eventDataforCard = data;  //updating global variable 
                buildAllCards(data, isEditable);

            },
            error: function (error) {
                console.log(error);
            }

        })
    }

    function buildAllCards(eventdata, isEditable) {
        console.log("card building..");
        //console.log(eventdata);
        console.log(eventDataforCard);
        var eventsDiv = $('#eventList');
        eventsDiv.empty()

        $.each(eventdata, function (index, event) {
            console.log(`from for each:${JSON.stringify(event)}`);
            let built = buildCard(event, isEditable);
            eventsDiv.append(built);
        });

    }

    function buildCard(event, isEditable) {
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

                               

                                <button class="btn btn-primary details me-2" data-eventid="${event.eventId}" data-eventname="${event.eventName}">Details</button>
                                
                         
                          `;
        if (isEditable) {
            card += `<button class="btn btn-warning edit ms-2 me-2" data-eventid="${event.eventId}">Edit</button>
                                <button type="button" id="delete-${event.eventId}" class="btn btn-danger deleteEvent ms-2" data-id =${event.eventId} data-name="${event.eventName}" ><i class="bi bi-trash-fill"></i></button>`;

        }
        //right div and container div closing
        card += `   </div>

                        </div>`;
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
    //get user's accessible event and Build cards
    function fetchAndBuildEventrow(eventId, isEditable, container) {
        $.ajax({
            url: `https://localhost:7239/api/event/getEventById/${eventId}`,
            type: 'GET',
            success: function (data) {

                console.log(data);
               
                var builtCard = buildCard(data, isEditable);
                container.append(builtCard);

            },
            error: function (error) {
                console.log(error);
                var noEvent = `<h3 class="text-secondary mt-4 text-center">There are No Events You have access to </h3>`;
                container.append(noEvent)
            }

        })
    }
    //get the events that are accessible by the current logged In user 
    function fetchUserAccessibleEventAndBuild(isEditable, container) {
        $.ajax({
            url: `https://localhost:7239/api/access/GetEventAccessForUser/${userId}`,
            type: 'GET',
            success: function (data) {
                $.each(data, function (index, object) {
                    console.log(object);
                    //using the event id get event details and build the card
                    fetchAndBuildEventrow(object.eventId, isEditable,container);
                })
            },
            error: function (error) {
                var noEvent = `<h4 class="text-secondary mt-4 text-center">You do not have access to any events </h4>`;
                container.append(noEvent)
                console.log(error);
            }
        })
    }

    //used in access management for selecting the event to grant access

    function BuildEventOptions(ele, events) {
        console.log(events);
       
        $.each(events, function (index, obj) {
            var option = ` <li class="dropdown-item">
                                    <label>
                                        <input type="checkbox" value="${obj.EventId}">
                                        ${obj.EventName}
                                    </label>
                                </li>`;
            ele.append(option)
        })
    }
    let mySelectedItems = [];
    let selectedItem = [];
    function handleCB(event, action) {
        const checkbox = event.target;
        const eventName = $(checkbox).closest('label').text().trim();
        console.log(eventName);
        console.log(checkbox);
        if (checkbox.checked) {
            mySelectedItems.push(checkbox.value);
            selectedItem.push(eventName)

        } else {
            mySelectedItems =
                mySelectedItems.filter((item) => item !== checkbox.value);
            selectedItem = selectedItem.filter((item) => item !== eventName);
        }

        $('#multiSelectDropdown').text(mySelectedItems.length > 0
            ? selectedItem.join(', ') : 'Select Items');

        $('#multiSelectRevokeDropdown').text(mySelectedItems.length > 0
            ? selectedItem.join(', ') : 'Select Items');
    }

    //ajax call to create access on event
    function GrantAccessToEvent(userId, eventId) {
        console.log("user : " + userId);
        console.log("event : " + eventId);
        $.ajax({
            url: "https://localhost:7239/api/access/create",
            type: 'POST',
            contentType: "application/json",
            data: JSON.stringify({
                id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                userId: `${userId}`,
                eventId: `${eventId}`
            }),
            success: function (data) {
                console.log(data);
            },
            error: function (error) {
                console.log(error);
            }
        })
    }

    //ajax call to revoke access to event

    function RevokeAccessToEvent(userId, eventId) {
        console.log("user : " + userId);
        console.log("event : " + eventId);
        $.ajax({
            url: `https://localhost:7239/api/access/delete/${userId}/${eventId}`,
            type: 'DELETE',
            
            success: function (data) {
                console.log(data);
            },
            error: function (error) {
                console.log(error);
            }
        })
    }

    //ajax call to assign role to the user
    function setUserRole(userId, role) {
        $.ajax({
            url: `https://localhost:7239/api/account/setUserRole/${userId}/${role}`,
            type: 'PUT',
            success: function (data) {
                console.log(data)
            },
            error: function (error) {
                console.log(error)
            }
        })
    }

    switch (page) {
        case 'Events':
            console.log(role);
            var eventsDiv = $('#eventList');
            if (role === "Owner") {
                loadEventData(true);

            }
            else if (role === "EventManager") {

                eventsDiv.empty();
                fetchUserAccessibleEventAndBuild(true, eventsDiv);
            }
            else if (role === "EventViewer") {

                eventsDiv.empty();
                fetchUserAccessibleEventAndBuild(false, eventsDiv);
            }
            else {
                loadEventData(false);

            }

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
            $('#eventTitle').text(name + " Locations");
            //on clicking the row in the location table
            $(document).on('click', '.location-table-data', function () {
                locationId = $(this).data('locid');
                console.log(locationId);
                console.log(eventDetailId);
                sessionStorage.setItem('locationId', locationId);
                window.location.href = `/location/StaffsInEvent/${eventDetailId}/${locationId}`;

            });
            $(document).on('click', '#addLocation', function () {
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

        case 'AccessManagement':
            console.log('access');
            var ddContainer = $('#eventsDropdownContainer');
            var revokedbContainer = $('#revokeeventsDropdownContainer');
            $(document).on('click', '.GrantAccessModal', function () {

                var userId = $(this).data('id');
                console.log($(this));
                console.log(userId);
                $('#grantAccessConfirm').data('id', userId);

                console.log(allEvents)//global variable from the view script
                console.log(accesses);
                //filter all the events that are not accessible by the user to show in options
                var granted = accesses.filter(function (acc) {
                    return acc.UserId == userId;
                });
                console.log(granted)

                var grantedEventId = granted.map(e => e.EventId);
                console.log(grantedEventId)

                var ungrantedEvents = allEvents.filter(function (eve) {
                    return !grantedEventId.includes(eve.EventId);
                })
                console.log(ungrantedEvents);
                ddContainer.empty();

                BuildEventOptions(ddContainer, ungrantedEvents)


                $('#AccessModal').modal('show');

            });

            $(document).on('click', '.RevokeAccessModal', function () {
                var userId = $(this).data('id');
                console.log($(this));
                console.log(userId);
                $('#revokeAccessConfirm').data('id', userId);
                var granted = accesses.filter(function (acc) {
                    return acc.UserId == userId;
                });
                console.log(granted)

                var grantedEventId = granted.map(e => e.EventId);
                console.log(grantedEventId)

                var grantedEvents = allEvents.filter(function (eve) {
                    return grantedEventId.includes(eve.EventId);
                })
                revokedbContainer.empty();
                BuildEventOptions(revokedbContainer, grantedEvents);

                $('#RevokeModal').modal('show');

            })

            $('.dropdown-menu').on('change', handleCB);

            $('#grantAccessConfirm').on('click', function () {
                var user = $(this).data('id');
                $.each(mySelectedItems, function (index, obj) {
                    GrantAccessToEvent(user, obj);
                })
                //clearing for the next usage
                $('#AccessModal').modal('hide');
                location.reload();
                mySelectedItems = [];
                selectedItem = [];
            })
            $("#revokeAccessConfirm").on('click', function () {
                var user = $(this).data('id');
                console.log(mySelectedItems);
                console.log(user);
                $.each(mySelectedItems, function (index, obj) {
                    RevokeAccessToEvent(user, obj);
                })
                $("#RevokeModal").modal('hide');
                location.reload();
                mySelectedItems = [];
                selectedItem = [];
            });

            //fetchUsersAndBuildUserTable();
            break;

        case 'RoleManagement':
            $(document).on('click', '.assignRole', function () {
                var id = $(this).data('id');
                $('#assignConfirm').data('id', id);
            })
            $(document).on('click', '.ManageRole', function () {
                var modifyModal = $('#ModifyModal');
                var currentRole = $(this).data('role');
                var userid = $(this).data('id');
                console.log(currentRole);
               // var filteredRoles = roleOption.filter(r => r.Text !== currentRole);
                //console.log(filteredRoles);
                var role = $('#currentRole')
                role.append(currentRole)
                var options = ``;
                $.each(roleOption, function (index, object) {
                    console.log(object);
                    if (object.text !== currentRole) {
                        options += `<option value="${object.value}">${object.text}</option>`;
                    }
                    
                })
                modifyModal.find('#changeRoleSelect').html(options);
                $('#modifyConfirm').data('id',userid)
                modifyModal.modal('show');
            });
            $('#assignConfirm').on('click', function () {
                var userId = $(this).data('id');
                var selectedRole = $('#assignRoleSelect :selected').val();
                console.log(selectedRole);
                console.log(userId);
                setUserRole(userId, selectedRole);
                $('#assignRoleSelect :selected').attr('selected', '');
                $('#assignModal').modal('hide');
                location.reload();
            })
            $('#modifyConfirm').on('click', function () {
                var userId = $(this).data('id');
                var selectedRole = $('#changeRoleSelect :selected').val();
                console.log(selectedRole);
                console.log(userId);
                setUserRole(userId, selectedRole);
                $('#changeRoleSelect :selected').attr('selected', '');
                $('#ModifyModal').modal('hide');
                location.reload();
            })
            break;
    }
 



})