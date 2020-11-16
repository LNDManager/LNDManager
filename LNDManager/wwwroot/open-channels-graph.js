window.renderOpenChannelsGraph = function (connectedChannels, node, connectedNodes) {

    let connectedNodesDictionary = connectedNodes.map(item => item.node).reduce((previous, current) => {
        return {
            ...previous,
            [current.pubKey]: current
		}
    }, {});

    let ourNode = { data: { id: node.identityPubkey, label: node.alias } };
    let nodes = connectedChannels.channels.map(channel => ({
        data: {
            id: channel.remotePubkey,
            label: connectedNodesDictionary[channel.remotePubkey].alias || channel.remotePubkey,
            selected: false,
            selectable: false,
            locked: true,
            grabbable: false
        }
    }));
    nodes.push(ourNode);

    let edges = connectedChannels.channels.map(channel => ({
        data: {
            id: channel.channelPoint,
            source: channel.initiator ? node.identityPubkey : channel.remotePubkey,
            target: !channel.initiator ? node.identityPubkey : channel.remotePubkey,
		}
    }))

    var cy = cytoscape({
        container: document.getElementById('cy'), // container to render in

        elements: [ // list of graph elements to start with
            ...nodes,
            ...edges
        ],
        style: [ // the stylesheet for the graph
            {
                selector: 'node',
                style: {
                    'background-color': '#666',
                    'label': 'data(label)'
                }
            },

            {
                selector: 'edge',
                style: {
                    'width': 3,
                    'line-color': '#ccc',
                    'target-arrow-color': '#ccc',
                    'target-arrow-shape': 'triangle',
                    'curve-style': 'bezier'
                }
            }
        ],

        layout: {
            name: 'circle'
        }

    });
}