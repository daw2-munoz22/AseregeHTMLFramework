export const config = {
    port: 44376,
    getPort: function () {
        return JSON.parse(JSON.stringify(this.port));
    }
}
