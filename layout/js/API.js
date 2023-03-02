export class API {
    async httpGetAsync(theUrl, callback) {
        try {
            const response = await fetch(theUrl);
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            const responseData = await response.text();
            callback(responseData);
        } catch (error) {
            console.error(error);
        }
    }

    async httpPostAsync(theUrl, data, callback) {
        try {
            const response = await fetch(theUrl, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            });
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            const responseData = await response.text();
            callback(responseData);
        } catch (error) {
            console.error(error);
        }
    }
}