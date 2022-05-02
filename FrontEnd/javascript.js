let row = 4;
function myFunction(row) {
var table = document.getElementById("tab");
var row = table.insertRow(row);
var name = row.insertCell(0);
var discovery = row.insertCell(1);
var extinction = row.insertCell(2);
name.innerHTML = "Neandertals";
discovery.innerHTML = "Fire";
extinction.innerHTML = "Dinosaurus";
row++;

}
function myFunction2(row) {
var table = document.getElementById("tab");
var row = table.insertRow(row);
var name = row.insertCell(0);
var discovery = row.insertCell(1);
var extinction = row.insertCell(2);
name.innerHTML = "Sarma";
discovery.innerHTML = "Me";
extinction.innerHTML = "Meat";
row++;
}

function myDeleteFunction(row) {
  document.getElementById("tab").deleteRow(row);
}

function popup() {  
document.body.style.backgroundImage = "url('images/panda.JPG')"; 
alert("Background picture changed");

};  
 
function Submit()
{
  checkName=document.getElementById("name").value;
  if( document.getElementById("name").value.length == 0 || document.getElementById("discovery").value.length == 0|| document.getElementById("extinction").value.length == 0 )
  {
    alert("Insert all information");
	return false;
  }
  else
  {
   
  document.getElementById("tab");
  var table = document.getElementById("tab");
  var row = table.insertRow();
  var name = row.insertCell(0);
  var discovery = row.insertCell(1);
  var extinction = row.insertCell(2);
  name.innerHTML = document.getElementById("name").value;
  discovery.innerHTML = document.getElementById("discovery").value;
  extinction.innerHTML = document.getElementById("extinction").value;
  }


}

function PrintList() {
    document.getElementById("printList").innerHTML = "";
    myName = "";
    myAddress = "";
    myPhone = "";
    myName = "<div class='Card' onclick='RemoveList(" + this.id + ")'><b>Name: </b>" + this.name + "<br>\n";
    myAddress = "<b>Address: </b>" + this.address + "<br>\n";
    myPhone = "<b>Phone: </b>" + this.phone + "</div><hr>\n";


    listPerson.forEach(function (listPerson, index) {
        var table = document.getElementById("tab");
        var row = table.insertRow();
        var name = row.insertCell(0);
        var discovery = row.insertCell(1);
        var extinction = row.insertCell(2);
        name.innerHTML = listPerson.name ;
        discovery.innerHTML = listPerson.address;
        extinction.innerHTML = listPerson.phone;

        document.getElementById("printList").innerHTML += "<div class='Card' onclick='RemoveList(" + index + ")'><b>Name: </b>" + listPerson.name + "  " + "<b>Address: </b>" + listPerson.address + "   " + "<b>Phone: </b>" + listPerson.phone + '<hr style="border-top: 3px solid #bbb"></hr>';"</div>";
        
    });

}
class Person {
    constructor(id, name, address, phone) {
        this.id = id;
        this.name = name;
        this.address = address;
        this.phone = phone;
        this.PrintList = PrintList;
    }
}

function CreatePerson() {
    checkName = document.getElementById("name").value;
    checkAddress = document.getElementById("address").value;
    checkPhone = document.getElementById("phone").value;
    if (checkName.length == 0 ||checkAddress.length == 0 ||checkPhone.length == 0) {
        alert("Unesi sva polja");
        return false;
    }
    let person = new Person(id, document.getElementById("name").value, document.getElementById("address").value, document.getElementById("phone").value);
    // var table = document.getElementById("tab");
    // var row = table.insertRow();
    // var name = row.insertCell(0);
    // var discovery = row.insertCell(1);
    // var extinction = row.insertCell(2);
    // name.innerHTML = document.getElementById("name").value;
    // discovery.innerHTML = document.getElementById("address").value;
    // extinction.innerHTML = document.getElementById("phone").value;

    listPerson.push(person);
    console.log(listPerson);
    ClearFields(tab);
    person.PrintList();
    id++;
}
function ClearFields() {

    document.getElementById("name").value = "";
    document.getElementById("address").value = "";
    document.getElementById("phone").value = "";
}
function RemoveList(myId) {

    let confirmAction = confirm("Jesi siguran da zelis obrisati?");
    if (confirmAction) {
        alert("Action successfully executed");
    } else {
        alert("Action canceled");
        return;
    }
    listPerson.splice(myId, 1);
    console.log(myId);
    PrintList();
}

let id = 1;
let listPerson = [];