const fetchTasks = async () => {
    const ressponse = await fetch("Todo/all-tasks", {method: "GET"});
    const data = await ressponse.json();

    return data;
}

const toggleTask = async (taskId) => {
    const response = await fetch(`Todo/toggle-task/${taskId}`, {method: "PATCH"});
    if (response.ok) renderTasks(await fetchTasks());  
}

const deleteTask = async (taskId) => {
    const response = await fetch(`Todo/delete-task/${taskId}`, {method: "DELETE"});
    if (response.ok) renderTasks(await fetchTasks());
}

const createTask = (li, task) => {
    li.id = task.id;
    const titleTag = li.querySelector(".title");
    titleTag.textContent = task.title;
    titleTag.addEventListener("click", () => toggleTask(task.id));
    titleTag.style.textDecoration = task.isDone ? "line-through" : "none";
    li.querySelector(".delete").addEventListener("click", () => deleteTask(task.id));
    
    return li;
}

const renderTasks = (tasks) => {

    const allTasks = tasks.map((task) => {
        const template = document.getElementById("task-template");
        const clone = template.content.cloneNode(true);
        const li = clone.querySelector("li");
        
        return createTask(li, task);
    })

    document.getElementById("todo-list").querySelector("ul").replaceChildren(...allTasks);
}

const addTask = async (title) => {
    if(!title) return;

    const response = await fetch("Todo/add-task", {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({title})
    });

    if(response.ok) renderTasks(await fetchTasks());
}

const main = async () => {
    const addTaskForm = document.querySelector("form");
    
    addTaskForm.addEventListener("submit", async (event) => {
        event.preventDefault();
        const input = addTaskForm.querySelector("input");
        const title = input.value.trim();
        addTask(title);
        input.value = "";
    });

    renderTasks(await fetchTasks());
}

window.addEventListener("load", main);
