// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.



// Write your JavaScript FETCH() code.

function getEmps() {
    var baseUrl = "http://localhost:5018/api/Employees/";
    fetch(baseUrl)
        .then(response => response.json())
        .then(data => {
            //console.log(data)
            //display the result into web page
            displayTble(data);
        })
        .catch(err => { console.log(err) });
}

function getEmpById() {
    var id = document.getElementById("ecode").value;

    var baseUrl = "http://localhost:5018/api/Employees/"+id;
    fetch(baseUrl)
        .then(response => response.json())
        .then(data => {          
            //display the result into web page
            document.getElementById("ename").value = data.ename;
            document.getElementById("salary").value = data.salary;
            document.getElementById("deptid").value = data.deptid;
        }).catch(err => { console.log(err) });
}
function addEmp() {
    var ec = document.getElementById("ecode").value;
    var en = document.getElementById("ename").value;
    var sal = document.getElementById("salary").value;
    var did = document.getElementById("deptid").value;

    var empObj = {
        "ecode": ec,
        "ename": en,
        "salary": sal,
        "deptid": did
    };

    fetch('http://localhost:5018/api/Employees/', {
        method: 'POST',
        body: JSON.stringify(empObj),
        headers: {
            'Content-type': 'application/json;charset=UTF-8'
        }
    }).then(response => {
        document.getElementById("msg").innerHTML = "Record inserted";
        getEmps();
    })
        .catch(err => {
            document.getElementById("msg").innerHTML = err
        });
}

function updateEmp() {
    var ec = document.getElementById("ecode").value;
    var en = document.getElementById("ename").value;
    var sal = document.getElementById("salary").value;
    var did = document.getElementById("deptid").value;

    var empObj = {
        "ecode": ec,
        "ename": en,
        "salary": sal,
        "deptid": did
    };

    fetch('http://localhost:5018/api/Employees/'+ec, {
        method: 'PUT',
        body: JSON.stringify(empObj),
        headers: {
            'Content-type': 'application/json;charset=UTF-8'
        }
    }).then(response => {
        document.getElementById("msg").innerHTML = "Record updated";
        getEmps();
    }).catch(err => {
        document.getElementById("msg").innerHTML = err
    });
}

function deleteEmpById() {
    var id = document.getElementById("ecode").value;
    var baseUrl = "http://localhost:5018/api/Employees/"+id;
    fetch(baseUrl,{
            method: "DELETE"
        }).then(response => {
            document.getElementById("msg").innerHTML = "Record deleted";
            getEmps();
        }).catch(err => {
            document.getElementById("msg").innerHTML = "could not delete";
        });
}



//AJAX requests to API

function getEmpsAJAX() {
    $.ajax({
        url: 'http://localhost:5018/api/Employees/',
        type: "GET",
        success: function (res) {
            //console.log(res);
            displayTble(res);
        },
        error: function (err) {
            console.log("some error");
        }
    });
}

function addEmpAJAX() {
    var ec = document.getElementById("ecode").value;
    var en = document.getElementById("ename").value;
    var sal = document.getElementById("salary").value;
    var did = document.getElementById("deptid").value;

    var empObj = {
        "ecode": ec,
        "ename": en,
        "salary": sal,
        "deptid": did
    };


    $.ajax({
        url: 'http://localhost:5018/api/Employees/',
        type: "POST",
        //dataType: "json",
        data: JSON.stringify(empObj),
        contentType: 'application/json',
        success: function (res) {
            document.getElementById("msg").innerHTML = "Record inserted";
            getEmpsAJAX();
        },
        error: function (err) {
            document.getElementById("msg").innerHTML = err;
        }
    });
}

function deleteByIdAJAX() {
    var id = document.getElementById("ecode").value;
    //send AJAX DELETE Request to call WEB API
    $.ajax({
        url: 'http://localhost:5018/api/Employees/' + id,
        type: "DELETE",
        success: function (res) {
            document.getElementById("msg").innerHTML="Record deleted:" + id;
            getEmpsAJAX();
        },
        error: function (err) {
            document.getElementById("msg").innerHTML="some error:could not delete the record";
        }
    });

}

function updateAJAX() {

    var ec = document.getElementById("ecode").value;
    var en = document.getElementById("ename").value;
    var sal = document.getElementById("salary").value;
    var did = document.getElementById("deptid").value;


    var empObj = { "ecode": ec, "ename": en, "salary": sal, "deptid": did };

    $.ajax({
        url: 'http://localhost:5018/api/Employees/' + ec,
        type: "PUT",
        contentType: "application/json",
        data: JSON.stringify(empObj),
        success: function (res) {
            document.getElementById("msg").innerHTML = "Record updated";
            getEmpsAJAX();
        },
        error: function (err) {
            document.getElementById("msg").innerHTML = "Failed to update the record";
        }
    });
}
function getByIdAJAX() {
    var id = document.getElementById("ecode").value;
    //alert('hello');
    $.ajax({
        url: 'http://localhost:5018/api/Employees/' + id,
        type: "GET",
        success: function (res) {
            //console.log(res);
            document.getElementById("ename").value = res.ename;
            document.getElementById("salary").value = res.salary;
            document.getElementById("deptid").value = res.deptid;

        },
        error: function (err) {
            console.log(err);
        }
    });
}


//==========================================================
//utility methods
function displayTble(data) {
    var elemStr = "<table border=4 width='60%'>";
    elemStr += "<tr><th>Ecode</th><th>Ename</th><th>Salary</th><th>Deptid</th></tr>";
    for (var i = 0; i < data.length; i++) {
        elemStr += "<tr><td>" + data[i].ecode + "</td>";
        elemStr += "<td>" + data[i].ename + "</td>";
        elemStr += "<td>" + data[i].salary + "</td>";
        elemStr += "<td>" + data[i].deptid + "</td>";
        elemStr += "</tr>";
    }
    //display the result
    document.getElementById("d1").innerHTML = elemStr;
}