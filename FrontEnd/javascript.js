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
//             {
//                 document.getElementById("tab").style.display="block";
                
//                 var table = document.getElementById("tab");
//                 var row = table.insertRow(4);
//                 var name = row.insertCell(0);
//                 var discovery = row.insertCell(1);
//                 var amt = row.insertCell(2);
//                 name.innerHTML = document.getElementById("name").value;
//                 discovery.innerHTML = document.getElementById("discovery").value;
//                 extinction.innerHTML = document.getElementById("extinction").value;
                
//                 return false;
//             }

//     // Add products to <table>
// function productsAdd() {
//         // First check if a <tbody> tag exists, add one if not
//         if ($("#productTable tbody").length == 0) {
//             $("#productTable").append("<tbody></tbody>");
//         }
        
//         // Append product to the table
//         $("#productTable tbody").append("<tr>" +
//             "<td>Extending Bootstrap with CSS, JavaScript and jQuery</td>" +
//             "<td>6/11/2015</td>" +
//             "<td>http://bit.ly/1SNzc0i</td>" +
//             "</tr>");
            
//         $("#productTable tbody").append("<tr>" +
//             "<td>Build your own Bootstrap Business Application Template in MVC</td>" +
//             "<td>1/29/2015</td>" +
//             "<td>http://bit.ly/1I8ZqZg</td>" +
//             "</tr>");
// }
