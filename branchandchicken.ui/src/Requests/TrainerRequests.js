import axios from 'axios';

const baseUrl = 'http://localhost:61848/api';

const getAllTrainers = () => new Promise((resolve, reject) => {
    axios.get(`${baseUrl}/trainers`)
    .then(result => resolve(result.data))
    .catch(err => reject(err));
});

export default getAllTrainers