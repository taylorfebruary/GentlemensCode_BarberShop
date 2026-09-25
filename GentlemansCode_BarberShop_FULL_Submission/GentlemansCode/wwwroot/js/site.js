function toggleMenu(){const n=document.getElementById('mainNav'),b=document.querySelector('.menu-toggle');const open=n.classList.toggle('open');b.setAttribute('aria-expanded',open);}
function closeWelcome(){const m=document.getElementById('welcomeModal');if(m){m.classList.add('hidden');sessionStorage.setItem('gcWelcomeSeen','1');}}
window.addEventListener('DOMContentLoaded',()=>{const m=document.getElementById('welcomeModal');if(m&&sessionStorage.getItem('gcWelcomeSeen'))m.classList.add('hidden');if(m)m.addEventListener('click',e=>{if(e.target===m)closeWelcome()});});
