

const createbtn = document.querySelector('.createbtn');
createbtn.addEventListener('click',(e)=>{
    
    e.preventDefault();
    createbtn.classList.remove('btn');
        createbtn.classList.add('onclick');
            setTimeout(function() {
        createbtn.classList.add('validate');
        createbtn.classList.remove('onclick');


        setTimeout(function() {
            createbtn.classList.remove('validate');
            createbtn.classList.add('btn');
    
    
        
                }, 2000 );



    
            }, 2000 );


})


const taskcategorycheckbox = document.querySelector('.taskcategorycheckbox');
const taskcategorydropdown = document.querySelector('.taskcategory-form-input');

console.log(taskcategorydropdown)
taskcategorycheckbox.addEventListener('Onchange',()=>{
    taskcategorydropdown.classList.toggle("show");
    console.log("ok")

})

